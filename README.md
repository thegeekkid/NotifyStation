# Notify Station

![Active Notification Screenshot](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Alert_Window.png "Active Notification Screenshot")

This software is designed to be used with either desktop/taskbar shortcuts, or something like a stream deck to alert users on another computer that they need are needed to answer communications (primarily in a live production environment - similar idea to a clearcom flasher).  Eventually this will be adapted to work with Luxafor indicators; however, shipping has been delayed, and I don't have any to test with for now... so it is just a Windows application.

The stations will need to connect to an API server... one is provided for free by default, but is rate limited using the token bucket algorithm, so if you have more than a handful of computers connecting to the server, you may want to spin up your own server and adjust the rate limiting to avoid any issues.

## Basic pre-compiled configuration

### Installation and configuration
1. Download the installer from the [releases section](https://github.com/thegeekkid/NotifyStation/releases).
1. Run the executable
![execute](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/01-execute.png "Execute")
1. If prompted, accept the UAC prompt.
![UAC prompt](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/02-UAC.png "UAC prompt")
1. Click past the welcome screen.
![Welcome screen](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/03-Welcome_Screen.png "Welcome screen")
1. Accept the EULA/License agreement.
![EULA](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/04-EULA.png "EULA")
1. Confirm your installation directory.
![Install Directory](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/05-Install_Path.png "Install Directory")
1. Confirm the shortcut folder settings.
![Shortcut folder](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/06-Shortcut_Folder.png "Shortcut Folder")
1. Confirm that everything looks good, then click "Next >".
![Confirmation page](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/07-confirm.png "Confirmation page")
1. Click "Finish".
![Final install screen](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/08-Finish.png "Final install screen")
1. Open your start menu and launch the "NotifyStation_GUI_Config" program from your shortcut folder.
![Launch the configurator](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/01-Installation/09-LaunchConfig.png "Launch the configurator")
1. When you first launch after installing, it will automatically reach out to https://api.notifystation.com and obtain a computer ID. No PII is sent with this request, and the computer ID is a randomized string used only within this application. The only two fields in the database are "ID" and "Call active", there is no identifying data included.  If you would like to change to a self-hosted API server, simply change the URL listed in API Server (be sure to include https://), then click "Re-generate Computer ID".
  - You can change your computer ID as often as you want; just be aware you will need to update your shortcuts on all connecting computers when you do so.
1. Record your computer ID somewhere safe (probably somewhere you can copy and paste from like a flashdrive), you will need this when setting up caller shortcuts on other computers.
1. Similarly, record your call.exe path, as you will need this when setting up shortcuts on the *current* computer.
1. If you want Notify Station to automatically start up in the background when the user signs in, click the checkbox for "Run At Startup" (it needs to be running if you want to receive notifications on this computer; it does *not* need to be running if you only want to call out to other computers).
1. When you are done configuring as desired, click "Save config and launch Notify Station"

### Creating a native Windows Shortcut to call another computer
1. Right-click on your desktop, then hover over "New" and select "Shortcut".
![New Shortcut](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/01-Right-click.png "New Shortcut")
1. Paste the call.exe path you recorded in the installation and configuration section; then click "Next".
![Paste the path](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/02-Paste_path.png "Paste the path")
1. Enter a good identifying name, then click "Finish".
![Enter the shortcut name](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/03-Name.png "Enter the shortcut name")
1. Right click your new shortcut, then click "Properties"
![Open properties](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/04-Properties.png "Open properties")
1. At the end of the "Target" field (after the quotation marks if applicable), add a space, then paste in the computer ID of the station you want to call with this shortcut.
![Add arguments](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/05-Add_argument.png "Add arguments")
  1. If you want to change the icon, you can also click the "Change Icon" button.
  ![Change icon button](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/5.5-Change_icon.png "Change icon")
  1. You can either select a built-in icon from Windows, or find your own .ico file to use, then click "OK".
  ![Select icon](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/5.6-Select_icon.png "Select icon")
1. Click "OK" on the main properties page.
![Click OK](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/6-ok.png "click OK")
1. Your shortcut is now ready to use!  Double click it and within a few seconds the notification should appear on the other computer.  You can also pin the shortcut to your taskbar or start menu for easier access.
![Pin to taskbar](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/07-Pin-taskbar.png "Pin to taskbar")
![Pin to start menu](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/02-Windows_shortcut/08-Pin_start.png "Pin to start menu")

### Creating a Stream deck shortcut to call another computer
1. Start by opening the Stream deck application from your system tray.
![Open stream deck](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/01-Launch_Stream_deck.png "Open stream deck")
1. Click the "More Actions" button on the lower right.
![More actions](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/02-More_Actions.png "More actions")
1. Search for and install the "Advanced Launcher" plugin.
![Advanced Launcher](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/03-Advanced_launcher.png "Advanced Launcher")
1. Once the plugin has installed, closed the "More Actions" panel.
![Close panel](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/04-close.png "Close panel")
1. Drag the "Advanced Launcher" option to the desired slot on your stream deck.
![Drag and drop](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/05-Drag_tile.png "Drag and drop")
1. Name your launcher, then click "Choose file"
![Name and choose](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/06-Name_n_choose.png "Name and choose")
1. Paste the call.exe path you recorded in the installation and configuration section; then click "Open".
![Paste and open](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/07-paste_open.png "Paste and open")
1. In the "Arguments" section, enter the computer ID of the computer you wish to call with this launcher.
![Add arguments](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/08-Add_argument.png "Add Arguments")
1. If desired, select a new icon for the launcher.
![Select icon](https://raw.githubusercontent.com/thegeekkid/NotifyStation/master/Screenshots/03-Streamdeck/09-Set_icon.png "Select Icon")
1. Your launcher is now ready to use!  Run it and within a few seconds the notification should appear on the other computer.

## Advanced - manual installation and self-hosting

### Installing from custom compiled source
  - Section coming soon
  
### Installing a self-hosted API server
  - Section coming soon