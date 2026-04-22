== on_load ==
Welcome to Gnomies! Use WASD to move and Spacebar to jump.
-> END

VAR lock_down = false
== soil_bag_removed == 
"Thank yoose. I got stuck under that soil bag there afta tryin' to hide from dem evil trolls. Luckily yoose saved me- Say, I couldn't do it alone but if we work togetha we could maybe fight back against dem trolls."
-> control

==control ==
"You can switch between me and you if you press the arrow keys. You'll need me to get out of here."
-> after_get_up

== after_get_up ==

 {lock_down == false:
 "Tha' first step would to somehow knock that there lock off tha' door there. Open that and we can get out to tha' garden and save our brothers." 
 - else: 
 "Would ya look at that. Seems like all we gotta doose is step on those plates to open tha' door"
 }
-> END
== reach_pressure_plates ==
-> END

== open_door ==
"To the garden!"
-> end_of_level

== end_of_level ==
"Let's go kick some troll butt"
-> END
