var Xamarin = importNamespace("Xamarin.Forms");

function BuildLabel(text) {
    var label = new Xamarin.Label();
    label.Text = text;
    return label;
}

function BuildImage(imageSource) {
    var image = new Xamarin.Image();
}

var label = BuildLabel("Test Label");