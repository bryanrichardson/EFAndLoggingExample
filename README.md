EFAndLoggingExample
===================

Demonstrates updating a database, logging and sending emails when a record is deleted


Open the package manager console and run update-database to create the database.

All tests should go green.  There are tests for the models and the repository but no tests for the controller yet.

The website should come up when you run the solution.  
You can add, edit, and delete movies to the list and all calls to the controller except the initial page load are done via AJAX.


Logging is done with log4net.  Right now there is no config for this so it isn't really persisting the logs

Known Issues
---------------------
There is a bug with the AJAX where newly added movies don't get there edit and delete buttons properly wired up so you can't edit and delete
those movies until you refresh the page.

Emails are not actually sent on deletes but the tests are there to make sure that the calls to send the email are made.  
The mailer implementation of the IMailer interface is blank right now so I didn't have to mess with setting up a test mail server to actually send email.
