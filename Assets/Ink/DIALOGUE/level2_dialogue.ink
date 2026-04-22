== on_load ==
"Alright, we's gotta save s'more gnomies- but be careful not to wake them sleepin' trolls or fall in that lake!"
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
 
 VAR first_gnome_saved = true
 {first_gnome_saved == true:
 "My savior! You rescued me, so let me join you! You can pick me up and stack me to reach higher places. Just press E."
 - else:
 "Thank you for freeing me! Let's get out of here, let's get over the wall over there."
 }
 -> END
 
VAR saved_all = false
== end_of_level ==
{saved_all == true:
"Nice job gnomies, let's take a break and recoup at the shed. We can save s'more in the mornin."
- else:
"You can't leave 'ere without the other gnomes, head back and look around s'more."
} 
-> END
