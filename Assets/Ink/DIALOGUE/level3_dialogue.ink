== start_level_3 ==
"Shh...the trolls are up. Don't let them see ya or they'll chase ya and lock yous up."
-> END

== reach_wall ==
"Looks like you can throw someone ova that wall there and go 'round the back end...""
-> END

== get_key ==
"Nice a key! Go free them gnome!"
-> END

VAR saved_all = false
== reach_pressure_plates ==
 {saved_all == false:
 "Looks like these only activate when 3 gnomes stand on em...find the other gnomes!" 
 - else: 
 "Look at that! Somethin' opened up"
 }
-> END

== end_of_level3 ==
"Here's tha big one...let's go take back our garden!"
-> END
