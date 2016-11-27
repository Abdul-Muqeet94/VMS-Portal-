
function updateValidationTips(tipText) {
    $(".validationTips").text(tipText);

}

function checkLength(field, fieldName, minLength, maxLength) {
    if (minLength === maxLength && field.val().length > maxLength || field.val().length < minLength) {
        field.addClass("ui-state-error");
        updateValidationTips("Length of " + fieldName + " must be " +
            minLength + ".");
        return false;
    }
    else if (field.val().length > maxLength || field.val().length < minLength) {
        field.addClass("ui-state-error");
        updateValidationTips("Length of " + fieldName + " must be between " +
            minLength + " and " + maxLength + ".");
        return false;
    } else {
        return true;
    }
}

/*function checkRegexp(field, regexp, onInvalidTip) {
    if (!(regexp.test(field.val()))) {
        field.addClass("ui-state-error");
        updateValidationTips(onInvalidTip);
        return false;
    } else {
        return true;
    }
}*/




function initDeleteEmployeeModal(parentDivIdentifier, deleteURL) {


    var HTML = "\
        <div id='delete-dialog-form' class='jqUI-modal' title='Delete Employee'>\
        <p>Are you sure you want to delete the following Employee?</p>\
        <form>\
            <fieldset>\
            <label>Employee Name: <span id='delete-dialog-field-name'></span></label>\
            <label>Employee Code: <span id='delete-dialog-field-code'></span></label>\
            <input type='submit' tabindex='-1' style='position:absolute; top:-1000px'>\
            </fieldset>\
        </form>\
        </div>";

    $(parentDivIdentifier).append(HTML);

    function deleteItem() {

        $.ajax({
            type: "POST",
            url: deleteURL,
            data: {
                id: rowToRemove,
            },
            success: function (data) {
                // remove the row from the data table
                $('*[data-id="' + rowToRemove + '"]')
                    .parent()
                    .parent()
                    .remove();


                deleteDialog.dialog("close");
                $('#toast').html('Employee deleted successfully');
                $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);

            },
            error: function (event) {

                deleteDialog.dialog("close");
                $('#toast').html('There was an error. The database server might be down.');
                $('#toast').stop().fadeIn(400).delay(3000).fadeOut(400);
            },
            complete: function (data) {


            },
            dataType: 'text json'
        });
    }

    deleteDialog = $("#delete-dialog-form").dialog({
        autoOpen: false,
        height: 200,
        width: 350,
        modal: true,
        buttons: {
            "Delete": deleteItem,
            Cancel: function () {
                deleteDialog.dialog("close");
            }
        },
        close: function () {

            //form = dialog.find("form");
            //form[0].reset();
            //allFields.removeClass("ui-state-error");
        }
    });

    toReturn = function (id) {
        // setting the global variable, this
        // is used in deleteItem()
        rowToRemove = id;

        // get the name and the description of the item that we are removing.

        var firstName = $('*[data-id="' + rowToRemove + '"]').parent().parent().children().filter(':nth-child(1)').html();
        var lastName = $('*[data-id="' + rowToRemove + '"]').parent().parent().children().filter(':nth-child(2)').html();

        var employeeCode = $('*[data-id="' + rowToRemove + '"]').parent().parent().children().filter(':nth-child(3)').html();

        var name = firstName +" "+ lastName;
        

        $('#delete-dialog-field-name').html(name);
        $('#delete-dialog-field-code').html(employeeCode);


        deleteDialog.dialog("open");
    };

    // When the enter key is pressed
    // prevent the form from being submitted.
    deleteDialog.find("form").on("submit", function (id, name, description) {
        event.preventDefault();
        deleteItem();


    });


    return toReturn;


}

function initEditEmployeeModal(updateURL) {

    // the modal is found in #employeeModals

    // first name and last name should not have extra space characters.
    var fieldFirstname = $('#modal_first_name');

    var fieldLastname = $('#modal_last_name');

    // employee code should not have any non numeric values
    var fieldEmployeeCode = $('#modal_employee_code');

    var fieldEmailAddress = $('#modal_email');

    var fieldAddress = $('#modal_address');

    var fieldMobileNo = $('#modal_mobile_no');

    var fieldDateOfJoining = $('#modal_date_of_joining');

    var fieldDateOfLeaving = $('#modal_date_of_leaving');

    var selectFunction = $('#modal_function');

    var selectDesignation = $('#modal_designation');

    var selectDepartment = $('#modal_department');

    var selectTypeOfEmployment = $('#modal_type_of_employment');

    var selectGrade = $('#modal_grade');

    var selectAccessGroup = $('#modal_access_group');

    var selectRegion = $('#modal_region');

    var selectLocation = $('#modal_location');


    // all the fields that need to be verified.
    var allFields = $([])
    .add(fieldFirstname)
    .add(fieldLastname)
    .add(fieldEmployeeCode)
    .add(fieldEmailAddress)
    .add(fieldAddress)
    .add(fieldMobileNo)
    .add(fieldDateOfJoining)
    .add(fieldDateOfLeaving)
    .add(selectFunction)
    .add(selectDesignation)
    .add(selectDepartment)
    .add(selectTypeOfEmployment)
    .add(selectGrade)
    .add(selectAccessGroup)
    .add(selectRegion)
    .add(selectLocation);




    function updateItem() {
        allFields.removeClass("ui-state-error");

        var valid = true;
        valid = valid && checkLength(fieldEmployeeCode, "Employee Code", 1, 10);
        //valid = valid && checkLength(fieldDescription, "description", 0, 100);
        valid = valid && checkSubmit('#employee-modal-form');
        if (valid) {


            // do an ajax call
            $.ajax({
                type: "POST",
                url: updateURL,
                data: {
                    id: rowBeingEdited,
                    first_name: fieldFirstname.val(),
                    last_name:fieldLastname.val(),
                    employee_code:fieldEmployeeCode.val(),
                    email:fieldEmailAddress.val(),
                    address:fieldAddress.val(),
                    mobile_no:fieldMobileNo.val(),
                    date_of_joining:fieldDateOfJoining.val(),
                    date_of_leaving:fieldDateOfLeaving.val(),
                    function_id:selectFunction.val(),
                    designation_id:selectDesignation.val(),
                    department_id:selectDepartment.val(),
                    type_of_employment_id:selectTypeOfEmployment.val(),
                    access_group_id:selectAccessGroup.val(),
                    region_id:selectRegion.val(),
                    grade_id:selectGrade.val(),
                    location_id:selectLocation.val()
                },
                success: function (data) {

                    // set first_name
                    $('*[data-id="' + rowBeingEdited + '"]')
                        .parent().parent().children().filter(':nth-child(1)')
                        .html(fieldFirstname.val());

                    // set last_name
                    $('*[data-id="' + rowBeingEdited + '"]')
                        .parent().parent().children()
                        .filter(':nth-child(2)')
                        .html(fieldLastname.val());

                    // set employee_code
                    $('*[data-id="' + rowBeingEdited + '"]')
                        .parent().parent().children()
                        .filter(':nth-child(3)')
                        .html(fieldEmployeeCode.val());

                    // set function
                    $('*[data-id="' + rowBeingEdited + '"]')
                        .parent().parent().children().filter(':nth-child(4)')
                        .html($("#modal_function option:selected").text());

                    // set designation
                    $('*[data-id="' + rowBeingEdited + '"]')
                        .parent().parent().children().filter(':nth-child(5)')
                        .html($("#modal_designation option:selected").text());
                    

                  
                    editDialog.dialog("close");
                    $('#toast').html('Record updated successfully');
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
                dataType: 'text json'
            });

        }
        return valid;
    }

    editDialog = $("#employee-edit-form").dialog({
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
            allFields.removeClass("ui-state-error");
        }
    });

    toReturn = function (id) {
        // set the rowBeingEdited global.
        rowBeingEdited = id;

        var employeeCode = rowBeingEdited;
        /**

            Do an ajax call to get all the parameters for the employee specified by employeeCode.
            
            if the status is false do not open the dialog.
            else load all the drop downs with the recieved function,designation, etc
            options. set first_name last_name etc and open the form.

            // open the dialog
            editDialog.dialog("open");


                {
                    id: ''
                    first_name: ''
                    last_name: ''
                    employee_code: ''
                    email: ''
                    address: ''
                    mobile_no: ''
                    date_of_joining: ''
                    date_of_leaving: ''
                    function_id: ''
                    designation_id: ''
                    department_id: ''
                    type_of_employment_id: ''
                    access_group_id: ''
                    region_id: ''
                    grade_id: ''
                    location_id: ''
                }

        **/


        // do an ajax call
        $.ajax({
            type: "POST",
            url: '/HR/EmployeeManagement/GetEmployee',
            data:{
                employee_id:employeeCode
            },
            success: function (data) {
                

                if(data.success) {
                    
                    fieldFirstname.val(data.first_name);
                    fieldLastname.val(data.last_name);
                    fieldEmployeeCode.val(data.employee_code);
                    fieldEmailAddress.val(data.email);
                    fieldAddress.val(data.address);
                    fieldMobileNo.val(data.mobile_no);
                    fieldDateOfJoining.val(data.date_of_joining);
                    fieldDateOfLeaving.val(data.date_of_leaving);


                    // I made a function for conciseness.
                    function setDropDownVal(dropDown, id, name) {
                        if (name == null || id == null) {
                            return;
                        }
                        dropDown.prop('disabled', true).trigger("chosen:updated");
                        dropDown.empty();
                        //<option value="0">none</option>
                        dropDown.append("<option value='" + id + "'>" + name + "</option>");
                        dropDown.append("<option value='0'>none</option>");
                        dropDown.val(id);
                        dropDown.prop('disabled', false).trigger("chosen:updated");

                    }

                    setDropDownVal(selectFunction, data.function_id, data.function_name);
                    setDropDownVal(selectDesignation, data.designation_id, data.designation_name);
                    setDropDownVal(selectDepartment, data.department_id, data.department_name);

                    selectAccessGroup.val(data.access_group_id)

                    setDropDownVal(selectGrade, data.grade_id, data.grade_name);
                    setDropDownVal(selectLocation, data.location_id, data.location_name);
                    setDropDownVal(selectRegion, data.region_id, data.region_name);
                    setDropDownVal(selectTypeOfEmployment, data.type_of_employment_id, data.type_of_employment_name);

                    // open the dialog
                    editDialog.dialog("open");

                } else {


                    editDialog.dialog("close");
                    $('#toast').html('Error in fetching data for this employee code.');
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