﻿using System;

namespace OjoDeSauronDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputerVisionQuickStart sauronseye = new ComputerVisionQuickStart();
            int countofpersons = sauronseye.ReviewPersons();

            
        }
    }
}
