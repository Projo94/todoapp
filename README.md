# TodoApi

* The TODO list should contain specific lists that the user can create, edit and delete.

* Each list should consist of a title, description and date properties (we can call them daily lists).

* Each list may have many tasks. Each task should have a title, description, deadline which is timestamp (this should be converted to chosen timezone) and done properties,


## The following endpoints should be implemented:
- **Login**

- **Fetch all user lists** (filters by date[daily lists] and title; pagination 10 items per page)

- **Create, Edit, Delete** list

- **Fetch tasks that belong to a list** (filter by done, filter by deadline)

- **Create, Edit, Delete** task

- Update done/status property on task (mark as done or not)

- Update user's timezone (each user can choose one timezone and all timestamps should be in that timezone[task deadline property]. If the user chooses a different timezone when fetching data, all timestamps should be automatically adjusted).

- The user should get an email notification about how many tasks they've completed that day. The email should be sent at midnight according to the time zone the user has selected.
