using UnityEngine;

using Cave;

public class WiiController : MonoBehaviour
{

	//Wiimote1@localhost:3883
	public bool useWii = true;
	public string wiiServerAdress = "localhost";
	public int wiiServerPort = 3883;


	public WiiMote WiiMote { get; private set; }
	/* Zur Benutzung des Controllers muss sich vorher die Instanz geholt werden*/
	public static WiiController Instance { get; private set; }

	void Awake(){
		Instance = this;
	}

	void Start()
	{
		try
		{
			if (this.useWii)
			{
				string adress = "Wiimote1@" + wiiServerAdress + ":" + wiiServerPort;
				Logger.Log("Verbinde zu WiiMote: " + adress);
				WiiMote = new WiiMote(adress);
			}
		}
		catch (System.Exception e)
		{
			Logger.LogException(e);
		}
	}

	/// <summary>
	/// Jedes Grafik-Frame updaten. Bitte den Unterschied zwischen Update und FixedUpdate ansehen:
	/// http://answers.unity3d.com/questions/10993/whats-the-difference-between-update-and-fixedupdat.html
	///
	/// Bedeutet: Die ToggleUp/Down-Status sind beim Drücken/Loslassen jeweils ein ganzes Frame vorhanden.
	/// Deswegen sollten alle Interaktionen mit der Wii, die auf Toggle-States basieren, in MonoBehaviour.Update()
	/// geschehen.
	/// </summary>
	void Update()
	{
		try
		{
			if (this.useWii)
			{
				WiiMote.update();
			}
		}
		catch (System.Exception e)
		{
			Logger.LogException(e);
		}
	}

	void OnDestroy()
	{
		WiiMote = null;
	}


	/*
	 * Die Hier Folgenden Methoden Können genutzt werden um ganz einfach den Status einzelner Tasten oder Analogelemente
	 * abzufragen. Bei den Tasten wird zwischen einem einmaligen Druck und der möglichkeit die Taste zu halten unterschieden.
	 * Methoden welche "hold..." heißen ermöglichen das Gedrückt halten. Wird die jeweilig andere Methode genutzt muss
	 * die Taste immer neu gedrückt werden
	 * */

	public bool buttonUp(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.UP) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonUp(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.UP) == ButtonState.DOWN;
	}

	public bool buttonDown(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.DOWN) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonDown(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.DOWN) == ButtonState.DOWN;
	}

	public bool buttonRight(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.RIGHT) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonRight(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.RIGHT) == ButtonState.DOWN;
	}

	public bool buttonLeft(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.LEFT) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonLeft(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.LEFT) == ButtonState.DOWN;
	}

	public bool buttonA(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.A) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonA(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.A) == ButtonState.DOWN;
	}

	public bool buttonB(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.B) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonB(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.B) == ButtonState.DOWN;
	}

	public bool buttonC(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.C) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonC(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.C) == ButtonState.DOWN;
	}

	public bool buttonZ(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.Z) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonZ(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.Z) == ButtonState.DOWN;
	}

	public bool buttonStickDown(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_DOWN) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonStickDown(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_DOWN) == ButtonState.DOWN;
	}

	public bool buttonStickLeft(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_LEFT) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonStickLeft(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_LEFT) == ButtonState.DOWN;
	}

	public bool buttonStickRight(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_RIGHT) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonStickRight(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_RIGHT) == ButtonState.DOWN;
	}

	public bool buttonStickUp(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_UP) == ButtonState.TOGGLE_DOWN;
	}
	
	public bool holdButtonStickUp(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.STICK_DIGITAL_UP) == ButtonState.DOWN;
	}

	public bool buttonOne(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.ONE) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonOne(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.ONE) == ButtonState.DOWN;
	}

	public bool buttonTwo(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.TWO) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonTwo(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.TWO) == ButtonState.DOWN;
	}

	public bool buttonPlus(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.PLUS) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonPlus(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.PLUS) == ButtonState.DOWN;
	}

	public bool buttonMinus(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.MINUS) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonMinus(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.MINUS) == ButtonState.DOWN;
	}

	public bool buttonHome(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.HOME) == ButtonState.TOGGLE_DOWN;
	}

	public bool holdButtonHome(){
		return this.useWii && this.WiiMote.getButtonState(WiiMote.ButtonId.HOME) == ButtonState.DOWN;
	}

	public float getXAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.ACC_X);
	}

	public float getYAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.ACC_Y);
	}

	public float getZAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.ACC_Z);
	}

	public float getRoll(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.ACC_ROLL);
	}

	public float getPitch(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.ACC_PITCH);
	}

	public float getNunchukPitch(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.NUNCHUK_ACC_PITCH);
	}

	public float getNunchukRoll(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.NUNCHUK_ACC_ROLL);
	}

	public float getNunchukXAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.NUNCHUK_ACC_X);
	}

	public float getNunchukYAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.NUNCHUK_ACC_Y);
	}

	public float getNunchukZAcceleration(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.NUNCHUK_ACC_Z);
	}

	public float getStickX(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.STICK_X);
	}

	public float getStickY(){
		return this.WiiMote.getAnalogValue (WiiMote.AnalogId.STICK_Y);
	}
}
