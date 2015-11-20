# Clipboard Calculator

Evaluate Lambda Calculus expressions on the clipboard.

Run the WPF app. Copy a lambda expression to the clipboard from Notepad. Press Ctrl+Shift+L. This is a global hot key, so you don't need to leave Notepad. The equation on the clipboard will be replaced with one reduction step. Then paste it into Notepad on the next line. Keep going until the clipboard is emptied, indicating that no further reductions are possible.

Can't type a lambda? Here's one: λ

Try this expression:

(λn.λf.λx.f (n f x)) (λf.λx.f (f x))

That means "The successor of 2", or 2+1. It will evaluate to 3 after three steps.

## Roadmap

I created this app for "The Lambda Calculus in Notepad", a talk that I will debut at the Dallas Functional Programmers user group. I'll start with copy-paste, search-replace operations to demonstrate the mechanics of The Lambda Calculus. But once we get into the more complex expressions, I'll pull out this handy tool.

The next step will be to animate a visualization of the β-reduction or η-reduction taken at each step. This will aide in the understanding of The Lambda Calculus.

Finally, I intend to create a Lambda Touch app in the spirit of [Sean Berry's](http://www.regularberry.com/) Algebra Touch. I've already floated the idea past Sean, and he's cool with it. If you would like to help, please let me know.

[@michaellperry](http://twitter.com/michaellperry).