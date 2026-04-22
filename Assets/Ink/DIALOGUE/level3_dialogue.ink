== on_load ==
"Shh...the trolls are up. Don't let them see ya or they'll chase ya and lock yous up."
-> END

== reach_wall ==
"Looks like you can throw someone ova that fence and go 'round the back end...""
-> END

VAR have_key = true
== get_key ==
"Nice a key! Go free them gnomes!"
-> END

== open_cage ==
"Thank you for saving me!"
-> END

VAR saved_all = false
== open_door ==
 {saved_all == false:
 "Something opened! But don't leave without the other gnomies!" 
 - else: 
 "Look at that! Somethin' opened up down there- Let's head inside."
 }
-> END
== end_of_level ==
"Let's take back our garden!"
-> END
