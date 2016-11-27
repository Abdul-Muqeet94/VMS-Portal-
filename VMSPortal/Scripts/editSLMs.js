




function initEditSLMModal(updateURL) {


    


    function updateItem() {

        var groupEmployees = new Array();
        $('#modal-employees option:selected').each(function () {
            groupEmployees[groupEmployees.length] = $(this).val()+"";
        });

        // do an ajax call
        $.ajax({
            contentType: 'application/json',
            type: "POST",
            url: updateURL,
            data: JSON.stringify({
                slm_id: SLMID,
                slm_employees: groupEmployees
            }),
            success: function (data) {
                if (data.error == "") {
                   
                    editDialog.dialog("close");
                    $('#toast').html('Record updated successfully');
                } else {
                    editDialog.dialog("close");
                    $('#toast').html(error);
                }
                
                



                
                $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);

                //console.log(data);
            },
            error: function (event) {

                editDialog.dialog("close");
                $('#toast').html('There was an error. The database server might be down.');
                $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);
            },
            complete: function (data) {


            },
            dataType: 'json'
        });

    }
    

    editDialog = $("#edit-slm-form").dialog({
        autoOpen: false,
        height: 600,
        width: 300,
        modal: true,
        buttons: {
            "Update": updateItem,
            Cancel: function () {
                editDialog.dialog("close");
            }
        },
        close: function () {

            form = editDialog.find("form");
            form[0].reset();
        }
    });

    toReturn = function (id) {
        // set the rowBeingEdited global.
        SLMID = id;


        // do an ajax call
        $.ajax({
            type: "POST",
            url: '/HR/SuperLineManagers/getSLM',
            data: {
                id: SLMID
            },
            success: function (data) {


                if (data.success) {

                    function setMultiDropDown(multiDropDown, array) {

                        if (array == null) {
                            return;
                        }

                        multiDropDown.prop('disabled', true).trigger("chosen:updated");
                        multiDropDown.empty();
                        //<option value="0">none</option>

                        for (var i = 0; i < array.length; i++) {
                            multiDropDown.append("<option selected='selected' value='" + array[i].id + "'>" + array[i].text + "</option>");
                        }
                        

                        multiDropDown.prop('disabled', false).trigger("chosen:updated");

                    }

                    setMultiDropDown($('#modal-employees'), data.slm_employees);

                    


                    // open the dialog
                    editDialog.dialog("open");

                } else {


                    editDialog.dialog("close");
                    $('#toast').html('Error in fetching data for this SLM.');
                    $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);

                }




            },
            error: function (event) {

                editDialog.dialog("close");
                $('#toast').html('There was an error. The database server might be down.');
                $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);
            },
            complete: function (data) {


            },
            dataType: 'text json'
        });





    };

    // When the enter key is pressed
    // prevent the form from being submitted.
    editDialog.find("form").on("submit", function (id, name, description) {
        event.preventDefault();
        updateItem();


    });


    return toReturn;
}