Dwarven Conflicts (Unity 5)
===
Dwarven Conflicts game remade with online multiplayer compability.

Development guide
---
###Hit test
* Hittable areas (environment that should be walkable etc.) all need to be placed on the 'Obstacle' layer. The Player will make hittest on all objects placed on this 'Obstacle'-layer.
* Different players platforms will be placed on their own layor probably to restrain the other players from walking on other player's platforms. 

###Photon Unity Networking 
Host multiplayer matches in the cloud. 100 concurrent connections world-wide.
Lobby and room capabilities. 

An idea is to connect to rooms online directly in browser based on RESTful URLs.
example.com/dwco/#/aGtWF

Where the room is "aGtWF". for simple copy paste link to a friend to join right game since it's only a two player game.


Need to have
---
* Better, easier to use controls (Maybe controller support)
* LAN, Online multiplayer

Nice to have
---
* New sounds
* More scenery and different designed stages
* Powerups and special items
* New improved music
