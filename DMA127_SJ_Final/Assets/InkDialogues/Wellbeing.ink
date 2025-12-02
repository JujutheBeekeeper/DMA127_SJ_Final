=== getRest ===
Maybe I should take a nap
*[Yes (2 hours)]
    I sure need it
    ~ StartQuest("TakeARest")

*[No]
    It is okay, I can keep going.
- -> END

=== takeMeds ===
I need to take my medicine
*[Yes (1 energy)]
    I sure need it
    ~ StartQuest("TakeMeds")

*[No]
    It'll be fine...
- -> END