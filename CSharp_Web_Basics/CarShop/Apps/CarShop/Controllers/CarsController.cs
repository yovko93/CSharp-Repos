﻿using CarShop.Services;
using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public CarsController(ICarsService carsService, IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            ICollection<CarViewModel> model = new List<CarViewModel>();
            var userId = GetUserId();

            if (carsService.IsUserMechanic(userId))
            {
                model = carsService.All();
            }
            else
            {
                model = carsService.AllByUser(userId);
            }

            return this.View(model);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (usersService.IsUserMechanic(this.GetUserId()))
            {
                return this.Redirect("/Cars/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (usersService.IsUserMechanic(GetUserId()))
            {
                return Redirect("/Cars/All");
            }

            if (input.Image == null || input.Model == null || input.PlateNumber == null)
            {
                return this.Error("None of the fields can be empty");
            }

            if (string.IsNullOrWhiteSpace(input.Model) || input.Model.Length < 5 || input.Model.Length > 20)
            {
                return this.Error("Model must be between 5 and 20 characters long");
            }

            if (string.IsNullOrWhiteSpace(input.Image) || !input.Image.StartsWith("http"))
            {
                return this.Error("Please enter a valid image path");
            }

            if (!Regex.IsMatch(input.PlateNumber, @"^[A-Z]{2}\s*[0-9]{4}\s*[A-Z]{2}$"))
            {
                return this.Error("Please enter a valid plate number");
            }

            var userId = this.GetUserId();
            this.carsService.Add(userId, input.Model, input.Year, input.Image, input.PlateNumber);

            return this.Redirect("/Cars/All");
        }

    }
}
