=== goToClass ===
I have class today.
Should probably go.
*[Let's go (5 energy)]
    Alright
    ~ StartQuest("GoToClass")

*[Not today]
    I don't feel like I can today.
- -> END

=== doHomework ===
I have homework to complete.
It's a lot.
*[Let's work on it a little (2 energy)]
    I'll try to concentrate.
    ~ StartQuest("Homework")

*[It can wait.]
    It'll be fine.
- -> END