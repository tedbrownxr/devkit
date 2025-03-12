An Element is the building block of menus: a box-model object with content inside of it.
- A Label element is plain text
- A Button element has an action (and a label)
- A Value element has an interface to show and edit a value (these are customized per value type)

A Row is a collection of elements that are arranged left-to-right.
> Example: A Label element followed by an Integer Field Element

A Panel is a collection of Rows that are arranged top-to-bottom, from Header to Content to Footer categories and also within those categories.
If multiple panels are visible, they are arranged left-to-right, in order of history (oldest to newest).

A Column is a virtual construct (a list, not a class) representing all of the Elements of a certain index in a Row.
They are often used to set a uniform width for all Label elements.
