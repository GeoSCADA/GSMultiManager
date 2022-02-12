Please refer to the file LICENSE.txt for terms relating to this code

GS Multi Manager
================
This program and document are Copyright Schneider Electric 2022. All rights reserved. See the license 
terms file in this folder.

Introduction
------------
Managing users on more than one Geo SCADA system can be time-consuming. Each user on each system has a 
different password entry, and you need to reset the password on every system when a user forgets it.
(This is not an issue if you can use Active Directory, but this option is not applicable to all).
If some systems are remote and not available at the time you want to change the password, you would
need to remember to go back to it. This GS Multi Manager tools solves these problems by managing
password change. It is designed to be used by an administrator.

This tool has a Windows service which sets user passwords across Geo SCADA Systems, and a user interface
tool to enter new password data.

The service is configured to autostart when Windows starts.

The passwords are set both locally or across the network by configuring the system names which are to 
be checked. The system names together with the encrypted credentials needed to access each node are 
stored in the Windows registry. Server names and ports are read from the Systems.XML file set up by 
the Configure Connections tool.

Security
--------
This tool is provided as source code with a trial build. Measures have been taken to provide security
for the administrator user credentials and user credentials. They are stored encrypted in the Windows
registry, and transfer to a different host will render them invalid. You are recommended to review
all security measures around this tool's code and environment, and make any changes you need for your
environment.

Note that the service is configured by the installer to run under a Virtual Service Account. This 
account requires access to the Windows registry key:
  HKEY_LOCAL_MACHINE\SOFTWARE\Schneider Electric\GSMultiManager
It requires read/write access to the folder of user names to change, and a log file in:
  C:\ProgramData\Schneider Electric\ClearSCADA\GS Multi Manager

Similarly the configuration and operations interface program is to be used by a user with privileges to
write to the windows registry (to set the Geo SCADA administrative user credentials) and to write to
the above folder too.

We recommend that ACLs are added to the registry and folder.

Installation
------------
The tool requires Windows .Net 4.8 to be installed. It will not check this, so please ensure it is present. 
(The registry key 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\SKUs' lists them.)

Please log in as Administrator in order to install this tool. The installation kit is named GSMMInstaller.msi
Open this file to install. There are no options to choose from. If a new version is built you need to
uninstall and reinstall.

The installer will place files into the folder: 
  C:\Program Files (x86)\Schneider Electric\GS Multi Manager
It will install and start the service 'GS Multi Manager'.

The service will need to be set up and operated using the tool 'GS Multi Manager Config' for which a 
shortcut is placed in the Start Menu and on the Desktop.

Configuration
-------------
Configure the service by running the 'GS Multi Manager Config' utility. When running for the first time,
enter a blank system, username and password.

For each server, enter each system name, the administrator username and the password and click 'Add'. 
The system names should match the names in the Systems.XML file on the same computer.

The administrator user needs to have the privileges to reset passwords on the system. It is recommended 
that a new user is created for this purpose on each system, with security privileges only for the
folder(s) containing user accounts.

The details are encrypted, and this encryption is specific to the machine, such that export and import of 
these  credentials to another machine will not be possible. Details are stored in the registry in the key:
HKEY_LOCAL_MACHINE\SOFTWARE\Schneider Electric\GSMultiManager

The change password tool enables restart of the service using the Stop and Start buttons on the Config tool. 
The server's status in the utility is refreshed every 5 seconds.

Operation
---------
When starting the configuration and password setting tool, you must enter a valid system name, user and
password matching one of the systems configured in its list.

To change a password, enter a user name, password and click Change. An entry is created in the registry
and appears in the list of users. The service process will, each 10 seconds, read the list and attempt to
set the password on all systems, trying each server on the system consecutively.

If you click a user name in the list, the server list will indicate on which systems the passwords are
yet to be changed. The user will be removed from the list when all systems have been modified. If an error
prevents a password from being changed, the error information is logged in the text log (described below)
and the service will keep trying to change it.

The checkbox 'Enable' will change the state of the account all systems to either Enabled or Disabled.
This will be useful when a member of staff with access to multiple systems leaves.

Note that if there are no users on any system which match the username, then that user will be ignored on
that system, and an entry indicating this is added to the log.


Text log of user accounts changed
---------------------------------
Operation of the tool can be verified by checking the log file. This is stored in the folder:
  C:\ProgramData\Schneider Electric\ClearSCADA\GS Multi Manager
Success or failure is logged for each user and system, logging the server too. Passwords are not logged.
A new file is created per month.

Event Logging
-------------
Service status and errors are logged to the Windows Event Log, in the folder:
  Application and Services Logs/GSMultiManager
Information messages for the startup and first run will be logged, then only errors will be logged. Please
check this after setup to verify all is correctly operating. You may wish to set the service to restart on
any errors - do this in the Services applet, under the Recovery tab - set the three failure actions to
'Restart the Service'.

Future
------
The source code for this tool is provided. Possible extensions you, others or we could add are:
* User property synchronisation, copying the user account from a master system to other systems.
* Web user interface for a user to reset their own password (e.g. identifying themselves with TOTP or memorable answers).
* Synchronisation of other items across systems, such as mimic symbols or templates.

Support
-------
This code and build are provided without support. Please refer to the Schneider Electric Exchange web forums 
to get help.

February 2022


You can discuss these features in the SE Exchange forums.


