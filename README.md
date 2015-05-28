# DumbBots.Net
A simple programming game that allows editing of scripts while the game is still being run. The scripts control the AI of some combatants in a Quake III-style game. 

The game can be used as a tool to get people interested in coding, or simply as a competitive game between different code.

Some extensibility provided to create a simple sandbox environment. A "director" AI can be programmed to alter the dynamics of the game for a customizable experience. The "director" can also add custom entities that it would control. 

Scripts are currently written in-game in C#, but any .NET language should be supported. (See the downloads page for details on how to develop in other languages with Microsoft Visual Studio.)

### Videos ###

<a href="http://www.youtube.com/watch?feature=player_embedded&v=bSjWUueKhPE" target="_blank"><img src="http://img.youtube.com/vi/bSjWUueKhPE/0.jpg" 
alt="Features Demo" width="240" height="180" border="10" /></a>
> *Features Demo*

<a href="http://www.youtube.com/watch?feature=player_embedded&v=LjG5SWcGVtc" target="_blank"><img src="http://img.youtube.com/vi/LjG5SWcGVtc/0.jpg" 
alt="Extensibility Demo" width="240" height="180" border="10" /></a>
> *Extensibility Demo*

### Screens ###

![MainGame](http://mcsyko.github.io/Images/DumbBots/main_game.png)
> *Default death-match mode - fight in Quake III style arena*

![CustomGame](http://mcsyko.github.io/Images/DumbBots/zombie_blaster.png)
> *Zombie Blaster demo - example of the "Director" AI adding extra enemies to the game and altering some dynamics of gameplay*

![BasicCoder](http://mcsyko.github.io/Images/DumbBots/basic_coder.png)
> *Basic coder to introduce non-programmers to some basic programming concepts*

### Features ###
* Program scripts while game is in play to immediately view results
* Basic map editor
* Scintilla.NET code editor
* "Basic Coder" which allows non-programmers to create scripts with drag-and-drop ease
* Basic "intellisense" to quickly find method needed
* "Director" AI allows a customizable experience - change the game rules, load custom models, add enemies, etc
* Basic messaging system allows interaction between different AI's
* Powered by Irrlicht engine
* Keyboard control - control your player directly!

### Coding Challenge Ideas ###
* Use the Director AI to create a quad-damage pickup.
* Extend the Zombie Blaster demo to have zombies that become faster and deadlier as the rounds pass
* Create a maze, and reward the player who reaches the center target first.
