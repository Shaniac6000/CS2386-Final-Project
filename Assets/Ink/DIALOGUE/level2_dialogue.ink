== on_level2_load ==
"Alright, we's gotta save some more of our friends- but be careful not to wake them sleepin trolls!"
-> END

VAR have_key = false
== get_key == 
"Ah you found a key, you can use that to free a gnome from their cage now."

-> END

== open_cage ==

 {have_key == false:
 "Darn, it looks locked- maybe there's a key around here that opens it." 
 - else: 
 "It unlocked nice!"
 }
 -> saved_gnome
 
 == saved_gnome ==
 
 VAR first_gnome_saved = false
 {first_gnome_saved == true:
 "My savior! You rescued me, so let me join you! You can pick me up and throw me to reach higher places. Just press E."
 - else:
 "Thank you for freeing me!"
 }
 -> END
 

== end_of_level ==
"Nice job guy, let's take a break and recoup at the shed. We can save s'more in the mornin."
-> END
