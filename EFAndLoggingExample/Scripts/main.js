// TODO: Fix a bug where newly added movies don't have the edit and delete buttons wired up until you refresh the page

$(function () {
    $('#add-movie').click(function() {
        // Add li with input box for new movie
        $('#movies').append('<li id="add-movie-li"><div class="input-group"><input id="new-movie-name" type="text" class="form-control"><span class="input-group-btn"><button id="insert-movie" class="btn btn-default" type="button">Insert</button></span></div></li>');
        $('#insert-movie').click(function () {
            // Make the call to the action that actually does the insert into the database
            $.ajax({
                url: "/Home/Insert",
                data: {
                    movie: $('#new-movie-name')[0].value
                },
                type: "POST",
                dataType: "json",
                success: function (json) {
                    $('#movies #add-movie-li').remove();
                    $('#movies').append('<li id="movie-' + json.Id + '"><span id="movie-name">' + json.Name + '</span><span id="edit-movie-' + json.Id + '" class="btn btn-default">Edit</span><span id="delete-movie-' + json.Id + '" class="btn btn-default">Delete</span></li>');
                    wireUpDeleteButtons();
                    wireUpEditButtons();
                },
                error: function (xhr, status, errorThrown) {
                    alert("There was a problem adding the movie!");
                    console.log("Error: " + errorThrown);
                    console.log("Status: " + status);
                    console.dir(xhr);
                }
            });
        });
    });
    wireUpDeleteButtons();
    wireUpEditButtons();

    function wireUpEditButtons() {
        $('ul#movies').on("click", '#edit-movie', function () {
            // Get id
            var idString;
            var classes = $(this).attr('class').split(' ');
            $(classes).each(function () {
                if (this.indexOf('edit-movie-') > -1) {
                    idString = this.substring(11);
                }
            });

            // update li with input box
            $('#movies li#movie-' + idString).html('<div class="input-group"><input type="hidden" id="id" value="' + idString + '"><input id="movie-name-input" type="text" class="form-control" value="' + $('#movies li#movie-'+ idString +' span#movie-name')[0].innerText + '"><span class="input-group-btn"><button id="update-movie" class="btn btn-default" type="button">Update</button></span></div>');
            $('#movies li#movie-' + idString).attr('id','edit-movie-li');

            $('#update-movie').click(function() {
                $.ajax({
                    url: "/Home/Update",
                    data: {
                        id: idString,
                        movie: $('#movie-name-input')[0].value
                    },
                    type: "POST",
                    success: function(json) {
                        $('#movies #edit-movie-li').html('<span id="movie-name">' + json.name + '</span><span id="edit-movie" class="btn btn-default edit-movie-' + json.Id + '">Edit</span><span id="delete-movie" class="btn btn-default delete-movie-' + json.Id + '">Delete</span>');
                        $('#movies #edit-movie-li').attr('id', 'movie-' + json.Id);
                        wireUpDeleteButtons();
                        wireUpEditButtons();
                    },
                    error: function(xhr, status, errorThrown) {
                        alert("There was a problem updating the movie!");
                        console.log("Error: " + errorThrown);
                        console.log("Status: " + status);
                        console.dir(xhr);
                    }
                });
            });
        });
    }

    function wireUpDeleteButtons()
    {
        $('ul#movies').on("click", '#delete-movie', function () {
            var idString;
            var classes = $(this).attr('class').split(' ');
            $(classes).each(function() {
                if (this.indexOf('delete-movie-') > -1) {
                    idString = this.substring(13);
                }
            });
            $.ajax({
                url: "/Home/Delete",
                data: {
                    id: idString
                },
                type: "POST",
                success: function() {
                    $('#movies #movie-' + idString).remove();
                },
                error: function (xhr, status, errorThrown) {
                    alert("There was a problem deleting the movie!");
                    console.log("Error: " + errorThrown);
                    console.log("Status: " + status);
                    console.dir(xhr);
                }
            });
        });
    }
});