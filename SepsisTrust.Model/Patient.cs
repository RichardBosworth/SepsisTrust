namespace SepsisTrust.Model
{
    /// <summary>
    ///     Represents a patient in the app.
    /// </summary>
    public class Patient
    {
        public string Initials { get; set; }
        public bool Gender { get; set; }
        public bool Pregnant { get; set; }
        public int Age { get; set; }
        public ClinicalArea Location { get; private set; }
    }
}