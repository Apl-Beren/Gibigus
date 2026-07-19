using Godot;

public partial class MainMenu : Control
{
	private Button _hostButton;
	private Button _joinButton;
	private LineEdit _ipAddressInput;
	
	// The port your game will communicate over (can be any open port)
	private const int Port = 8910; 

	public override void _Ready()
	{
		// 1. Grab references to your UI nodes
		_hostButton = GetNode<Button>("HostButton");
		_joinButton = GetNode<Button>("JoinButton");
		_ipAddressInput = GetNode<LineEdit>("IPaddress");

		// 2. Subscribe to the button press signals
		_hostButton.Pressed += OnHostPressed;
		_joinButton.Pressed += OnJoinPressed;
	}

	private void OnHostPressed()
	{
		var peer = new ENetMultiplayerPeer();
		// Host a server on the designated port, allowing a maximum of 4 players
		Error error = peer.CreateServer(Port, 4); 
		
		if (error == Error.Ok)
		{
			// Assign the peer to Godot's Multiplayer API
			Multiplayer.MultiplayerPeer = peer;
			GD.Print("Server hosted! Waiting for players...");
			
			// Note: After this, you would typically load your game map scene
		}
	}

	private void OnJoinPressed()
	{
		var peer = new ENetMultiplayerPeer();
		// Default to localhost (127.0.0.1) if the input is left blank for testing
		string ip = string.IsNullOrWhiteSpace(_ipAddressInput.Text) ? "127.0.0.1" : _ipAddressInput.Text;
		
		// Connect to the host's IP address
		Error error = peer.CreateClient(ip, Port);
		
		if (error == Error.Ok)
		{
			Multiplayer.MultiplayerPeer = peer;
			GD.Print("Joining game...");
		}
	}
}
