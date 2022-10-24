using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication99.Entities;
using WebApplication99.Helpers;
using WebApplication99.Models;

namespace WebApplication99.Controllers
{
    [Authorize(Roles ="admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class MemberController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public MemberController(DatabaseContext databaseContext, IMapper mapper, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberListPartial()
        {
            List<UserModel> users =
                _databaseContext.Users.ToList()
                    .Select(x => _mapper.Map<UserModel>(x)).ToList();

            return PartialView("_MemberListPartial", users);
        }


        public IActionResult AddNewUserPartial()
        {
            return PartialView("_AddNewUserPartial", new CreateUserModel());
        }

        [HttpPost]
        public IActionResult AddNewUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return PartialView("_AddNewUserPartial", model);
                }

                User user = _mapper.Map<User>(model);
                user.Password = _hasher.DoMD5HashedString(model.Password);

                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return PartialView("_AddNewUserPartial", new CreateUserModel { Done = "User added." });
            }

            return PartialView("_AddNewUserPartial", model);
        }

        public IActionResult EditUserPartial(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);

            return PartialView("_EditUserPartial", model);
        }

        [HttpPost]
        public IActionResult EditUser(Guid id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return PartialView("_EditUserPartial", model);
                }

                User user = _databaseContext.Users.Find(id);

                _mapper.Map(model, user);
                _databaseContext.SaveChanges();

                return PartialView("_EditUserPartial", new EditUserModel { Done = "User updated." });
            }

            return PartialView("_EditUserPartial", model);
        }

        public IActionResult DeleteUser(Guid id)
        {
            User user = _databaseContext.Users.Find(id);

            if (user != null)
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
            }

            return MemberListPartial();
        }
    }
}
