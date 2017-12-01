# LearningSystem

Models: 
• Students – have username, name, email, password and birthdate, list of courses
• Courses – have name, description, trainer (registered user with role trainer), start date, end date, list of students
• Articles – have title, content, publish date and author

Features: 
• Users can register by providing username, name, email, password and birthdate or alternatively by creating account with their Facebook or Google profile.
• Registered users can sign up to any course
• Registered users can sign up/out of course before its start date
• Registered users can read all articles from the blog
• Registered users can edit their profile
• Trainer of a course can place grades to each student signed up to it. Grades can be A, B, C, D, E or F (from highest to lowest)
• Trainer can assess a student for a course only when the course is over.
• Blog authors can publish articles in the blog. Once published the article cannot be modified.

Roles:
• Guest (unregistered users) – can view all courses and their details
• Student – That is the regular registered user. Can sign up to any course of their choice. 
• Trainer – can assesses students in a course that he is set as trainer
• Blog author –  can publish articles in the blog
• Administrator – can create new courses or edit existing ones and set roles to all users
