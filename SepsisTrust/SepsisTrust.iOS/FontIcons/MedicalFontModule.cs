using System.Collections.Generic;
using Plugin.Iconize;
using SepsisTrust.IconFonts;

namespace SepsisTrust.iOS.FontIcons
{
    public class MedicalFontModule : IconModule
    {
        public MedicalFontModule(  ) : base("medicalfont", "medicalfont", "medicalfont.ttf", MedicalFontCollection.Icons)
        {
        }
    }
}