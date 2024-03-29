﻿using System;
using DIContainer.Injectors;
using SillyGame.DI;
using SillyGame.IO.Contracts;

namespace SillyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureDI module = new ConfigureDI();
            Injector injector = new Injector(module);
            Engine engine = injector.Inject<Engine>();

            engine.Start();


            //Console.WriteLine(module.GetMapping<IReader>().Name);
            //Console.WriteLine(module.GetMapping<IWriter>().Name);
        }
    }
}
