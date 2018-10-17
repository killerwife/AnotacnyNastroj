using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Projekt.Interfaces;

namespace Projekt
{
    /// <summary>
    /// Tato trieda sluzi na zadefinovanie externych metod pre rozpoznavanie obrazu, ktore je mozne nasledne
    /// aplikovat na obrazky projektu.
    /// </summary>
    public static class ExternalMethodForRecognition
    {
        /// <summary>
        /// Metoda volana pri vyvolani pouzitia externych metod rozpoznavania obrazu v menu aplikacie
        /// </summary>
        /// <param name="project">umoznuje pristup k obrazkom projektu, a moznosti nastavit najdene objekty na obrazky</param>
        public static void ProcessExternalMethod(IExternalUseProject project)
        {
            var a = project.GetAllProjectImages();

            var rnd = new Random();
            var list = a.Select(variab => new List<BoundingBoxStructure> {new BoundingBoxStructure(new Rectangle(new Point(rnd.Next(100), rnd.Next(70)),new Size(rnd.Next(100), rnd.Next(100))), "Random")}).ToList();
            project.AddBoundingBoxesToImages(list);
        }
    }
}
