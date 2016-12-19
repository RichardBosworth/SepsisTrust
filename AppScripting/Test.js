var Xamarin = importNamespace("Xamarin.Forms");

function BuildLabel(text) {
    var label = new Xamarin.Label();
    label.Text = text;
    return label;
}

var label = BuildLabel("Alright mate");