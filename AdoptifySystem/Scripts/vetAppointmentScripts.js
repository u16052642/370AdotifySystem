//var Animals = []
////fetch Animals from database
//function LoadAnimal(element) {
//    if (Animals.length == 0) {
//        //ajax function for fetch data
//        $.ajax({
//            type: "GET",
//            url: '/VetAppointment/getAnimals',
//            success: function (data) {
//                Animals = data;
//                //render catagory
//                renderAnimal(element);
//            }
//        })
//    }
//    else {
//        //render animal to the element
//        renderAnimal(element);
//    }
//}

//function renderAnimal(element) {
//    var $ele = $(element);
//    $ele.empty();
//    $ele.append($('<option/>').val('0').text('Select'));
//    $.each(Animals, function (i, val) {
//        $ele.append($('<option/>').val(val.Animal_ID).text(val.Animal_Name));
//    })
//}

//fetch sicktype
//function LoadSickType(animalDD) {
//    $.ajax({
//        type: "GET",
//        url: "/VetAppointment/getSickType",
//        data: { 'Animal_Type_ID': $(animalDD).val() },
//        success: function (data) {
//            //render sicktype to appropriate dropdown
//            renderSickType($(animalDD).parents('.mycontainer').find('select.sicktype'), data);
//        },
//        error: function (error) {
//            console.log(error);
//        }
//    })
//}

//function renderSickType(element, data) {
//    //render sicktype
//    var $ele = $(element);
//    $ele.empty();
//    $ele.append($('<option/>').val('0').text('Select'));
//    $.each(data, function (i, val) {
//        $ele.append($('<option/>').val(val.VetAppReasonsID).text(val.Reason));
//    })
//}

$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#animal').val() == "0") {
            isAllValid = false;
            $('#animal').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#animal').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#reason').val() == "0") {
            isAllValid = false;
            $('#reason').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#reason').siblings('span.error').css('visibility', 'hidden');
        }


        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.animal', $newRow).val($('#animal').val());
            $('.reason', $newRow).val($('#reason').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#animal,#reason,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#vetAppItems').append($newRow);

            //clear select data
            $('#animal,#reason').val('0');
            $('#vetItemError').empty();
        }

    })

    //remove button click event
    $('#vetAppItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#vetItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#vetAppItems tbody tr').each(function (index, ele) {
            if (
                $('select.reason', this).val() == "0" ||
                ($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var vetItem = {
                    VetAppReasonsID: $('select.reason', this).val(),
                }
                list.push(vetItem);
            }
        })

        if (errorItemCount > 0) {
            $('#vetItemError').text(errorItemCount + " invalid entry in vet item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#vetItemError').text('At least 1 vet item required.');
            isAllValid = false;
        }

        if ($('#VetName').val().trim() == '') {
            $('#VetName').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#VetName').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#vetDate').val().trim() == '') {
            $('#vetDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#vetDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                VetName: $('#vetName').val().trim(),
                VetDateString: $('#vetDate').val().trim(),
                Description: $('#description').val().trim(),
                VetAppointment: list
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/VetAppointment/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#vetName,#vetDate,#description').val('');
                        $('#vetAppItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }

    });

});

LoadAnimal($('#animal'));
