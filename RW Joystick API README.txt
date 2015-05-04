
RailWorks 2 Joystick API (0.0.1) - Cadde 2011

This application and it's soruce is distributed for free!!!
If you bought this you have been ripped off!

The executable and it's source code comes with no warranty or support, if it breaks you cannot expect any person to fix it for you.
If it harms your computer in any way (which of course it shouldn't) you will have to fix it yourself and cannot be compensated in any way.
This software is/was originally coded by Cadde (there should only be one!) on his spare time and not much time has been spent errorproofing the code
and as such you will most likely encounter numerous bugs and possibly even a crash or two by the application.

--------------------------------------------------------------------------------------------------------

1. - Introduction

2. - Installing the software

3. - Features

4. - Managing the controls
4.1 - Setting up Throttle, Brakes, Reverser and other analog controls
4.1.1 - Slider
4.1.2 - Notched slider
4.1.3 - Notched slider with ranges
4.1.3 - Notched slider with targets
4.1.5 - Up/Down latch
4.1.6 - Up/Down relative
4.1.7 - Toggle
4.1.8 - Special handling of...

4.2 - Setting up Wipers, Headlights and other digial controls
4.2.1 - Not detailed...

5. Using the speedometers

6. Known bugs

7. Support

8. Resources/Links

9. For other programmers

10. Changelog



--------------------------------------------------------------------------------------------------------
	1.   INTRODUCTION
--------------------------------------------------------------------------------------------------------

This software is designed to use the API in RailWorks that RailDriver™ is using to connect their device to the game.
The software allows you to use a Joystick/Steering wheel/Gamepad etc in it's place to supply input to the game via analog and digital controls beyond the keyboard.

NOTE: Currently the software is in a FIRST PROTOTYPE stage, that means it hasn't even reached ALPHA (Where features are refined and added) and certainly not
beta (Where bugs are squashed and features are finalized) so don't expect too much... But it should work as intended after slapping it around a bit. =)



--------------------------------------------------------------------------------------------------------
	2.   INSTALLING THE SOFTWARE
--------------------------------------------------------------------------------------------------------

Make sure .Net Framework 3.5 is installed! This application will NOT run without it!
You can get .Net Framework 3.5 from here: http://www.microsoft.com/downloads/en/details.aspx?FamilyId=333325fd-ae52-4e35-b531-508d977d32a6&displaylang=en
If the above link doesn't work (Microsoft is known for being asshats) then just google ".Net Framework 3.5"!

Also... If you have Windows XP (or at the very least lack support for WPF) then you can't run this application. Sorry! Go install Windows 7 or open the source code and make a cross platform version!

1. Locate your game installation folder (Default location is "C:\Program Files(X86)\Steam\steamapps\common\railworks")
2. Open the "Plugins" folder and make sure there's a "RailDriver.dll" file in there.
3. Extract/Copy the "RailWorks_Joystick_API.exe" file into this folder.
4. Start the application by double clicking the "RailWorks_Joystick_API.exe" file or optinally create a shortcut on your desktop to the .exe by clicking it and holding down 
   with the right mouse button then dragging it to the desktop.
5. If the application refuses to start then make sure (again) that the "RailDriver.dll" file is present in the same folder as the "RailWorks_Joystick_API.exe" file.

If nothing seems to work then read the SUPPORT section.
But of course it WILL work so... PROFIT?



--------------------------------------------------------------------------------------------------------
	3.   FEATURES
--------------------------------------------------------------------------------------------------------

For an initial release there is quite a lot of fatures!

* Map joystick axles and buttons to train controls in RailWorks 2 (This is the original purpose of the application)
* Set up your analog inputs in any way you might need. (Well, almost)
* Use more than one device at the same time. (Using two joysticks etc)
* Read data from RailWorks 2 and present an analog speedometer AND a digital one. (This will only work if you have dual monitors or is running RailWorks in a window.)
* Another window with an analog speedometer twice the size. (This window can also be placed to cover the small analog speedometer and the digital one if you prefer)
* Read inputs from the devices and present them in the same window as the speedometer. (This window ONLY updates when RailWorks has focus... I.E you are playing the game!)
* Easy to use set up for controls that at the same time allows ADVANCED settings without hiding it from the user in some obscure location.
* Ability to save, load, duplicate and create several configurations on the fly inside the application.
* Other bits and pieces i have forgotten i made...
* OPEN SOURCE!



--------------------------------------------------------------------------------------------------------
	4.   MANAGING THE CONTROLS
--------------------------------------------------------------------------------------------------------

Setting up the controls is EASY! Every time you start the application you have an empty control mapping. If you have previously saved
a mapping it can be loaded here. Otherwise just click the "Show Controls" button in the top right corner of the main window.
You are now presented with all the available controls in this application and two speedometers, one analog and one digital.
The analog controls are at the very bottom of this window and are as follows:

* Throttle + Combined throttle/Brakes (This one controls BOTH inputs in the game)
* Train brakes (Shouldn't be used in conjunction with combined throttle and brake)
* Locomotive brakes
* Dynamic brakes
* Reverser
* Gear (For locomotives that have gears)

The remaining controls are inside a framed area above the digital speedometer and to the right of the analog speedometer.
Most are taken from the game's graphics and should be easy to recognize but there are a few that are "custom made" (badly because i got bored) for the controls
that doesn't have a graphical representation in the game GUI. (Graphical User Interface)

* Manual coupling
* Pass at danger Ahead/Behind
* Switch junction Ahead/Behind
* Parking brakes (Locomotive, only works on locos that have a functioning parking brake!)

Every control has a tooltip (holding the mouse pointer over them will reveal what they are) so you should be able to recognize them fairly easy.

Anyways... Right clicking a control will present an additional window that will tell you what game control you are setting up and present you
with a list (that will be empty if you haven't set up any controls) of all inputs acting on this game control.
The initial steps should be fairly self explanatory but if you can't figure it out...

Add Input - Creates an unassigned input.

Assign Selected - By selecting an input (added with "Add Input") and pressing this button you are now putting the input in "Listen mode".
Pressing ANY button or moving any control on any gameplay device (Joysticks etc) will assign that Button/Axis to that input. *

Remove Selected - Selecing a input in the list and pressing this button will remove the selected input. This cannot be undone but you can always load a previously
saved controlmap if you mess up. However, if you haven't saved you might wanna think it through before pressing the button as there is no confirmation! (at present)

* - At the time of writing, all axles and buttons need to be "initialized" by the application. This means that if you press "Assign Selected" and then press a
button (generally only buttons are affected) on your joystick/gamepad/whatever that hasn't been pressed since the application was started it will be set to a negative state.
A negative state means as long as you aren't pressing that button the input sent to the application will be 100% (representing a pressed button)
This can be useful in certain situations. (I.e, you want the reverser to always be in forward until you press and hold the button or whatever else you can think of really)
However, the functionality is just something added with axles in mind to flip the input. Moving it up will present a Negative direction and moving it down a positive.
I left it for buttons because one of the future features will be a "block input" type button that if not pressed then do nothing with the next input kinda deal.

Since version 0.0.5 there are two additional buttons... "Import" and "Export"!
These will save a particular selected input map to a file or load a previously saved input map from file.

By selecting the input you want to export and hitting the export button you will be presented with a save dialog. Type a descriptional file name and hit save.
Unless you decided to save this in another directory it should now be present in the folder where the application resides. This file can (just like settings files with the .CMAP extension)
be submitted to a forum or sent to a friend where they in turn can import it using the "Import button".

Pressing "Import" will present you with a load dialog. Browse to the file you want to import and hit load.
Now you are presented with yet another dialog asking you if you want to import the device assignment. Doing so will load the assigned device ID (the number of the device presented by the system)
and the mapped axis/button. If you don't have the same device or for that matter, your device doesn't hold the same device ID as the original author of the input map file then
you should press "NO" when asked. Then you assign your own input as either positive or negative. (depending on what the author has said)
Pressing cancel aborts the import procedure.
Now that the input map is inported the application will create a new input map and add it to the end of the list. If you had any input maps in the list before this then make sure you
remove them so they won't fight over control for the action in question. (Unless you know what you are doing and want both inputs)

!!! FINAL NOTE ON SETTING UP CONTROLS !!!

If you make a controlmap and it works then SAVE it and make BACKUPS of it. First time i set up mine i made a save and backup for every control i mapped successfully.
I have spent 2 days in a row playing with the working control maps without a crash so far but while i did the initial set up i had a crash on average every 2 controls i set up.
SO SAVE OFTEN!
Read the known bugs section... Many bugs present in the first (0.0.1) release are still present. See the change log section to find out which bugs have been squashed!


4.1 - Setting up Throttle, Brakes, Reverser and other analog controls
--------------------------------------------------------------------------------------------------------

By analog controls i am talking about things that can be moved in the control interface and thus is a percent type control.
These are normally mapped to your joysticks "analog" controls (controls that move freely in a certain direction) but also be mapped to buttons!
See the following sub-sections for details on each control mode.


4.1.1 - Slider

This is the most basic control type. Say you have a throttle control on your joystick, the output value will copy the input value at all times.


4.1.2 - Notched slider
--------------------------------------------------------------------------------------------------------

This is almost identical to the Slider BUT here you can define "snap points" which means the output will only be set to the percentage values you type into the
textbox. Each notch should be separated by a comma, and decimal points can be entered for finer control. Entering anything else will either crash the application or
at the very least give unexpected results! (Unless someone fixes it in a later build in a suitable way)

There is also a dropdown list where you can select premade evenly distributed notches. 

	Selecting 3 notches in the list will result in...
	Notches = "0, 50, 100"

	Selecting 5 notches in the list will result in...
	Notches = "0, 25, 50, 75, 100"

Do note that the "snap points" are selected by a "closest notch" function. If you have your Notches set to "0, 50, 100" then the game output will be:

	0% for any physical position between 0% and 25%.
	50% for any physical position between 50% and 75%.
	100% for any physical position between 75% and 100%.
	

4.1.3 - Notched slider with ranges
--------------------------------------------------------------------------------------------------------

Here you are given another textbox below the notches where you can enter what value you want for each notch in the input, for example:

	Notches = "0, 10, 48, 52, 90, 100"
	Ranges = "0, 0, 50, 50, 100, 100"
	
	This will result in three "deadzones" where the middle (48% to 52%) snaps to 50% output and the bottom/top 10% snaps to 0% and 100% respectively.
	
Another example:

	Notches = "0, 25, 48, 52, 75, 100"
	Ranges = "0, 40, 50, 50, 60, 100"
	
	Now we are left with one "deadzone" in the middle and two zones on either side with "fine control"...
	Moving the stick to 25% will result in an output of 40%. Any movement between 25% and 48% will result in an output value between 40% and 50%.
	In other words...
	
	OutputValue = 40 + ((InputValue - 25 / (48 - 25)) * (50 - 40))
	
	Putting the stick at 30% would result in a value of 42.17% in the output.
	This type of control is VERY useful when you are working with brakes! Setting a deadzone in the "brake lap" area and then having fine controls for more brakes/less brakes
	and finally rough controls for hard braking or brake release.
	
	NOTE: At present... Setting two notches at the SAME values (I,E "0, 20, 50, 50, 100") will have unexpected and potentially application crashing results!
	Avoid it at ALL times!
	
	
4.1.4 - Notched slider with targets
--------------------------------------------------------------------------------------------------------

Sometimes you want the power of a notched slider but you also want to define what values should be sent. Say you want a 25% application on the axis to send a value of 50% to
the game... This is what this control is designed to do!

While the same effect can be acheived with the "Notched slider with ranges" control with some tinkering, this makes that whole ordeal less painful.
An example:

	Notches = "0, 25, 60, 70, 80, 90, 100"
	Targets = "0, 35, 45, 55, 75, 85, 100"
	
	Moving your control to 25% of it's physical range will give the game a value of 35%!
	
Note that, just like the "Notched slider", this control utilizes a "closest notch" function. If you are unsure how this works then re-read the "Notched slider" sub section.


4.1.5 - Up/Down latch
--------------------------------------------------------------------------------------------------------

This control will ONLY change the output value if you move the stick up from 50% or down from 50% and will only ADD to the
output value if the input value is beyond the output value, for eaxmple...

	Notches = "0, 50, 100"
	Ranges = "0, 50, 100"
	
	Moving the stick up to 60% will set the output value to 60% and it will keep that value as you move the stick down to 50% again.
	Moving the stick down to 40% will first reset the output value to 50% and then decrease the output value to 40% as you move the stick.
	...
	Moving the stick down to 40% and back to 50% will keep the output value at 40%, moving the stick down to 45% will have NO CHANGE on the output value.
	Only moving the stick below 40% or above 50% will either increase or reset the value.
	
	The application has (as of the time of writing) been hardcoded to respond to 50.1% and 49.9% leaving a tiny deadzone in the middle. You can of course
	increase this deadzone using the "Notches" and "Ranges" textboxes but you cannot decrease it. But you can move it on the input side using the settings!


4.1.6 - Up/Down relative
--------------------------------------------------------------------------------------------------------

This control works the same was as the Up/Down latch with the exception that the value isn't reset to 50% when you move the input in the opposite direction.
Instead it adds any movement in either direction (beyond 50.1% and 49.9%) until it reaches 0% or 100%.

NOTE: For personal (Me, Cadde) reasons i have opted to DOUBLE the effect any movement has on the output. I.E, moving the input to 60% will add 20% to the output value.
The same way moving an axis from 50% to 100% where the output was 0% would leave the output at 100%.
Using the provided "Notches" and "Ranges" settings you can remedy this hardcoded change by making the ranges output half the values... Example:

	Notches = "0, 100"
	Ranges = "25, 75"


4.1.7 - Toggle
--------------------------------------------------------------------------------------------------------

This control is useful for when you want a specific input to change between two or more states.
Setting the "Targets" field to "0, 25, 60, 80, 100" will have the effect that the output value will be 0, 25, 60, 80, 100 percent in order.
Every time you move/press a button and the input is "positive" (meaning you are increasing it) the RW output value will toggle in order from first to last and then restart when it
has reached the end... For example:

	Assign a button (as positive) to an input.
	Treat as: Toggle
	Toggles = "0, 25, 50, 75, 100"
	
	Press the button once... You now have an output value of 25.
	Press the button another time... You now have an output value of 50.
	Press the button two more times and you get 75 first, then 100 as the output value.
	Pressing it again will set the output value to 0. And thus you have cycled all values!
	
It is also possible to cycle backwards through the list of values by assigning the button as "negative". To do this you must hold the button down while you press "Assign Selected".
Do note that the toggle responds to each buttons assigned "Toggles" and as such... Assigning "0, 50, 100" to the Positive and "0, 25, 50, 75, 100" to the negative will have the
effect that pressing the "positive" button will give you values "0, 50, 100" and pressing the negative will give you the values "100, 75, 50, 25, 0"
Make sure you copy over the values you want on both inputs (or make some clever use of this feature if you want some strange setups :) )

!!! NOTE !!!
Assigning an axis as a toggle is adviced against!
Doing so will cycle the toggle really fast as you move the axis control!

Finally, don't assign the same button/axis to a toggle on the same action, they will cancel eachother out! Unless you have separate values in the "toggles" field, but doing this
will just give you strange results that you might not want... Unless you somehow are trying to make another programming language that is really strange using this application.
Also, expect crashes if you mess with this in ways not intended!


4.1.8 - Special handling of...
--------------------------------------------------------------------------------------------------------

I (Cadde) have opted to handle the reverser differently as it sticks out from the norm. Instead of only accepting 0% to 100% inputs in RailWorks it accepts
-100% to 100% and as such i have taken whatever value that is given in output (0% to 100%) and subtracted 100% and added the original value times 2.
So for example...

	ValueSentToGame = -100 + (InputValue * 2)

It shouldn't bother you as you set up your controls but if you expected to set an output value of -100 to 100 and realized you CANT DO THAT...
It's because i am forcefully restricting ALL outputs to stay between 0 and 100% and therefore it is hardcoded in the application for the reverser instead.



4.2 - Setting up Wipers, Headlights and other digial controls
--------------------------------------------------------------------------------------------------------

These are normally meant for buttons on the joystick/gamepad but can be used with axis too.
For example, setting up a notched slider (0% and 100%) will result in an axis working like a button.
One good control for this is the "switch Junction" controls. Setting the input to the stick left/right
by using a "Notched slider with ranges" with the following settings...

	Junction Behind:
	Direction = "Negative"
	Notches = "0, 75, 76, 100"
	Ranges = "0, 0, 100, 100"

	Junction Ahead:
	Direction = "Positive"
	Notches = "0, 75, 76, 100"
	Ranges = "0, 0, 100, 100"

Do note that some buttons like the Bell doesn't do anything because they don't have a matching call in the raildriver API. If nothing happens when you right click
these it means the raildriver API can't send the command to Railworks anyways and can therefore not be assigned to avoid confusion.
In a later build this should be made clearer with a disctinct red cross over the functions that doesn't do anything.
	
4.2.1 - Not detailed...
--------------------------------------------------------------------------------------------------------

Buttons are ideantical to axis in every way except for the input which ALWAYS ranges from 0% to 100%.
You can use this to your advantage but i won't detail it because it's excessive.

Just a hint...

Throttle:
2 Inputs as 2 buttons.
	Input 1:
	Notches = "0, 100"
	Ranges = "50, 55"
	Input 2:
	Notches = "0, 100"
	Ranges = "45, 50"

Now, pressing the button assigned to input 1 will increase throttle by 10%
PRessing the button assigned to input 2 will decrease the throttle by 10%

YOU WIN?!?

--------------------------------------------------------------------------------------------------------
	5. USING THE SPEEDOMETERS
--------------------------------------------------------------------------------------------------------

There are three speedometers in the application...

1) The analog speedometer in the control setup screen.
2) The digital speedometer in the control setup screen.
3) The analog speedometer in a separate larger screen.

If you left click any of the analog speedometers you cycle through all available types... These are as of present:

* 120 MPH Daylight skin
* 120 MPH Orange nighttime skin
* 120 MPH Blue nighttime skin
* 200 MPH Daylight skin
* 200 KPH Daylight skin
* 300 KPH Daylight skin
* 360 KPH Daylight skin

When you have a KPH (Kilometers per hour) speedometer in the control setup screen then your digital speedometer also reads KPH values.
If you want to you can have a MPH or KPH speedometer on the control setup screen and the other type in the large speedometer window. Rather than limit you
to using just one type you have the freedom to pick whatever type you want in their separate screens.

The large speedometer window can also be placed over the control setup screens analog and/or digital speedometer.
The reason you would do this is to see the controls move in the control setup screen but block vision of the digital speedometer and if you are using the large speedometer
you might also want to block view of the small one. You don't have to do this but it was designed with that functionality in mind.


--------------------------------------------------------------------------------------------------------
	6. KNOWN BUGS
--------------------------------------------------------------------------------------------------------

Version 0.0.1 (PROTOTYPE)

	* Inputting text, special characters etc etc... Anything but 0 - 9, Comma (,), Decimal (.), and Space ( ) in "Notches" and "Ranges" text fields can have unexpected results and crash the application.
	* Giving a invalid file name for new controlmap files or when duplicating WILL ... I REPEAT  > W I L L < crash the application. No refunds!
	* Closing the application while the devices is giving input (I.E pressing buttons or moving the controls or leaving the controls in a "limbo" state) will produce a crash! 
	  You wanted to close it anyways BUT the autosave feature (the one that saves the "Last Used.CMap" file) will crash with it most likely...
	* ALT + TABBING in and out of RailWorks not only causes render bugs in RailWorks (that's bugs in the game) but can also lead to undesireable effects with this application like the horn staying on constantly.
	* On occation, RailWorks will fight over control of the train... Especially so when you just started a new scenario! This can be fixed by dragging a control in the HUD (The one that opens and closes
	  with F4) as it seems some scenarios try to preset certain controls until you drag them in the hud.
	* Giving the "Notches" and "Ranges" text fields crack will produce odd results... Try and stick with smaller to larger values (I.E "0, 1, 2, 3, 4" and NOT "2, 1, 4, 10, -1, banana bread")
	* Saving a control map that is broken will render it broken when you load it... In other words, if you suspect it's broken then remove it and start over or keep banging your head against the wall.
	* Renaming "hot lesbian porn.avi" to "pornmap.CMap" and loading it in the application WILL crash the application. (Obviously)
	* Complaining about the bugs in the application and the bugs listed here will make the application eat your firstborn.
	* Plenty of other bugs i have supressed from memory.

Version 0.0.2 (PROTOTYPE)

	* Under the debug tab on the main window there are two buttons for Next/Previous device... Currently they do NOTHING.
	* If for whatever reason you can't assign an axis but you know the device is working. Check the debug tab (with only one device connected) and make sure the Delta value can go above 1500.
	  If it can't then you have to (for now) open the source for "InputHandler.vb" and compile a new build with "Public AssignFilter As Integer = 500" (Near line 196) or smaller depending on your devices strange outputs.
	* Sometimes the game cannot keep up and doesn't respond to this application, in particular when getting the current speed. The result is the speedometer momentarily drops to 0 and when this happens it doesn't
	  mean your train is going at 0 MPH. Changing "InputHandler.vb" Line 192 "Public APIInterval As Integer = 33" to a higher value might rememdy this a little but will give you less responsive input.

Version 0.0.3 (PROTOTYPE)
	
	* No new known bugs...

Version 0.0.4 (PROTOTYPE)
	
	* Impossible to set new controls. (Fixed in 0.0.4_2 which bears the same name as 0.0.4, If you encountered this then you need the new 0.0.4_2 from forum thread. Addess in Resources section)
	
Version 0.0.5 (PROTOTYPE)
	
	* (Not really a bug) Loading control settings from an earlier version causes "Up/Down Latch" and "Up/Down Relative" to be assigned to "Notched Targets" And "Up/Down Latch" respectively.
	  You have to manually fix it by selecting the "Treat as" value below the one selected IF you loaded settings from an eariler version and the mappings are set to either "Notched Targets" or "Up/Down Latch".
	
	
--------------------------------------------------------------------------------------------------------
	7.   SUPPORT
--------------------------------------------------------------------------------------------------------

As has been already mentioned at the very top of this document, there is no hotline to call... There is no forum thread to bother with your mistakes or the shortcomings of the
application given to you. However, if you feel that you can somehow contribute while asking for a particular fix... That is if you are ready to follow these simple steps:

* Was it listed in the "KNOWN BUGS" section above? If so then STOP right here... Your inputs are not wanted!
* Search every little corner of the internet... BYTE by BYTE for a solution or if someone has already submitted the request/problem you have. If you found it then don't bother!
* Read this document until you can cite it without reference to an audience of 10 or more. (Well, no... but read it again. ALL OF IT! Just to be sure!)
* What did you try when you experienced a crash?
* What device(s) did you use? (Joysticks etc)
* Was there an error message? What did it say? (ALL OF IT DAMMIT!)
* Have you read the source code? Even if you don't understand it, just so you could pinpoint the location of the error?
* Did you give the application some wierd values or put it in wierd situations? (Like running it on Linux or a Mac or on your IPhone...)
* What results did you expect/want from experimenting with the applications settings or putting it in a blender?
* Are you aware of the fact this application is a PROTOTYPE and shouldn't work as per default?

With all that done, post a 10 page essay on what you learned from following the above list to the forum thread linked in the RESOURCES section.
If you post "Doesn't work, crashed... !!!!!!! *sadface* *stabbing pandas* =(" then you will be ignored... For all eternity and be put on my (Cadde's) *TO KILL* list. (Which i WILL work over someday... Possibly after being poked yet again by a person who doesn't know up from down.)

It's the best deal ever!



--------------------------------------------------------------------------------------------------------
	8. RESOURCES/LINKS
--------------------------------------------------------------------------------------------------------

Main forum thread and downloads: http://http://forums.uktrainsim.com/viewtopic.php?f=318&t=112879


--------------------------------------------------------------------------------------------------------
	9. FOR OTHER PROGRAMMERS
--------------------------------------------------------------------------------------------------------

Before you dig into the source code... If the latest release is the PROTOTYPE release 0.0.1 then you are in for a mess! If releases following that are based off 0.0.1 then you are in for possibly a bigger mess!
Also, please be aware of the following:

	* The programmer behind 0.0.1 (PROTOTYPE) is not an educated programmer. All he knows he has learned from testing and using google... ALOT... FOR DAYS ON END!
	* The application was written in Visual Basic .NET under Visual Studio 2010 using .Net Framework 3.5
	* If you have anything good or bad to say about the language chosen then don't tell me about it... Ever! (I am ashamed as it is already!)
	* The application started out nicely but lack of "Prior Planning Prevents Piss Poor Performace" or "PPPPPP" it has been turned into the scarred monster it is today. (I might remake it from scratch some day just for fun to make it PRETTY)
	* Because of the above line... It started out with the "Joystick" class and then went into the "InputHandler" class and then into the "ucControls" WPF user control. Then lots of punching kittens and pulling hairs until
	  it turned into something that actually worked. Mainly by changing everything up several times in ALL classes except the "Joystick" class.
	* As you will be able to tell by following the development i outlined i went from caring to just thinking "MAKE SHIT WORK DAMMIT".
	* ILikeCamelCase. But i follow my own naming and coding convention (That i make up as i go) so have fun with that. =)

Now, with all that said...

	frmMain - Uhh, the main window. Handles saving and loading and starting and stopping everything
	Joystick - Implements DirectX for DirectInput and listens for all game controllers inputs and attempts to provide it to InputHandler in a event driven fashion.
	InputHandler - Manages mappings (that is the relationship between a device input and the game controls) and sends + reads data to/from the game. It's the largest middle man in the entire application. It's also the largest MESS!
	frmControls - Basically a slave window for ucControls just holding a ElementHost and passing values along.
	ucControls - The GUI end of things. It's a mess!
	Globals - Things i want to have close to me at all times. Yeah...

In the source directory under "bin\debug\images" there are a number of image files. Mostly .png that are all set to be compiled into the exe as resources.
In the optional "Original images for developers" package there are a select few .psd files holding the analog and digital speedometer originals in their full layered glory.

If you after reading this section feel a strong urge to bitch and moan then let me remind you of 3 simple things...

1. I don't care about what you have to say... really!
2. I made something you (probably) didn't and released the source (which you most likely didn't) so if you are so great then make it yourself!
3. I made this for my OWN benefit! Still i spent some time making it user friendly and i also wrote this readme! I could just have made it for myself and said "screw you" to the world but i did not as you can see.

Have fun deciphering the code!
Non dumb questions are welcome!

--------------------------------------------------------------------------------------------------------
	10. CHANGELOG
--------------------------------------------------------------------------------------------------------

0.0.1
	* PROTOTYPE RELEASE
	* FIRST RELEASE EVER
	* Everything changed...
	
0.0.2
	* [FEATURE] Added another analog speedometer in a separate window twice the size.
	* [BUGFIX] Fixed switch junction ahead/behind so it now has more consistent results.

0.0.3
	* [FEATURE] Added "Shift all mapped devices Up/Down" functionality.
	   Pressing "Up" Causes all input maps that where previously assigned to DeviceID X to be assigned to DeviceID X + 1. Pressing down has the opposite effect!
	   This is great to have when you connect or disconnect devices and all your mappings are mapped to the wrong device.
	   !!! Warning !!!   If the device ID already is at 0 for a specific mapping it will be shifted to -1 and as such will be REMOVED. The same applies to ID's that are greater than the number of devices you have...
						 For example: You have 2 devices connected and most mappings are on the wrong DeviceID 0 but you also have some mapped to DeviceID 1. Using the "Up" button will cause those few to be set
						 from DeviceID 1 to DeviceID 2 but you only have 2 devices connected (ID's 0 and 1) so the ones that now get ID 2 will be REMOVED.
	  Obviously, save your shifted control maps separately until you can verify that the shift operation had the desired result!
	* [FEATURE] Programmed the buttons on the debug tab for "Previous Device" and "Next Device". Pressing these will cause the debug window to only update for the selected device.
	* [FEATURE] Added "Listen to all" button on the debug tab. Pressing this restores the debug to react to ANY device input. Filtering to one device can be useful if another device is "spazzing" out.
	* [BUGFIX] Fixed a bug causing multi devices to not work as expected. Using more than one device should now work again. (*Facepalm*)

0.0.4
	* [FEATURE] Added several analog speedometers... Both MPH and KPH ones. You can cycle through them by left clicking on them.
	* [FEATURE] The digital speedometer now reads MPH or KPH depending on what analog speedometer you have selected in the control setup screen.
	* [REQUIREMENTS] The speedometer additions makes the executable file considerably larger. (from 1.6 MB to 6.5 MB, this will change in a future build with external skins.)

0.0.4_2
	* [BUGFIX] Fixed a MAJOR bug that was introduced in 0.0.4 that caused all controls to become unresponsive to right clicks. They are now working as intended again!

0.0.5
	* [FEATURE] Added "NotchedTargets" control mode which is described in section 4.1.4 in the readme.
	* [FEATURE] Added "Toggle" control mode which is described in section 4.1.7 in the readme.
	* [FEATURE] Added Import/Export function for the control manager, you can now export/import specific input maps! This is detailed in section 4 in the readme.
	* [COMPATIBILITY] Due to the added control modes, older control setup files are incompatible with this release. You can still fix the problem by manually
	  going over each control and changing the control mode to the appropriate one... More details on doing this can be found in section 6 under known bugs for version 0.0.5 in the readme.
	  






















And they all lived happily ever after... I hope... THE END!