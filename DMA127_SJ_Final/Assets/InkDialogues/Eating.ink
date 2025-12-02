=== haveBreakfast ===
Let's have breakfast
*[Yeah (2 energy)]
    Good
    ~ StartQuest("MakeBreakfast")

*[Not yet]
    I'm not hungry yet
- -> END


=== haveLunch ===
Let's prepare lunch
*[Yes (3 energy)]
    Good, I was hungry
    ~ StartQuest("MakeLunch")

*[Not yet]
    It's okay
- -> END


=== haveDinner ===
I should start preparing dinner
*[Let's do it (5 energy)]
    ~ StartQuest("Dinner")

*[Not yet]
    I'm a little tired for it
- -> END