using Godot;

public partial class GameSettings : Node
{
    // Variables to store the loaded data
    public float RectWidth { get; private set; }
    public float RectHeight { get; private set; }

    public override void _Ready()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        var config = new ConfigFile();

        // Load the file
        Error err = config.Load("res://music and general/constants.cfg");

        if (err == Error.Ok)
        {
            // GetValue parameters: Section, Key, DefaultValue (if key is missing)
            RectWidth = (float)config.GetValue("RectSettings", "default_width", 100.0f);
            RectHeight = (float)config.GetValue("RectSettings", "default_height", 100.0f);
            
            GD.Print("Settings loaded successfully.");
        }
        else
        {
            GD.PrintErr("Could not load config file, using defaults.");
            RectWidth = 100.0f;
            RectHeight = 100.0f;
        }
    }
}