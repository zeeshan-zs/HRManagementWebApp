////$(document).ready(function () {
////    $('#POEmployeesIndex').DataTable();
////});


$(document).ready(function () {

    var POEmployeesListTable = $('#POEmployeesIndex').DataTable({
        //dom: 'Blfrtip',
        "initComplete": function () {
            $("#POEmployeesIndex").show();
        },
        buttons: [
            'csv', 'excel'
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            //debugger
            var CurrentDate = new Date(); //Get Current Date

            var GivenPptExpiryDate = aData[11]; //Get Passport Expiry Date from Table
            GivenPptExpiryDate = new Date(GivenPptExpiryDate); //Convert to to JS Date Format

            
            var ThirtyDaysPriorDate = aData[11]; //Get Passport Expiry Date from Table
            ThirtyDaysPriorDate = new Date(ThirtyDaysPriorDate); //Convert to to JS Date Format
            ThirtyDaysPriorDate.setDate(GivenPptExpiryDate.getDate() - 30); //Get 30 days prior date from Passport Expiry Date

            if (GivenPptExpiryDate < CurrentDate) { //check if expired
                //Passport has expired
                $(nRow).addClass('table-danger');
                //$('td:eq(2)', nRow).html('<b>Expired ppt</b>');
            }
            else if (ThirtyDaysPriorDate < CurrentDate) { //check if near expiry
                //Passport will expire in 30 days
                    //$('td:eq(2)', nRow).html('<b>Less than 30 days</b>');
            }            
        }
    });
    //append buttons to the top of the table bellow filter
    POEmployeesListTable.buttons().container().appendTo('#POEmployeesIndex_wrapper .col-sm-12:eq(0)');

    
    //Add spacing to search box
    var searchBoxDiv = $('#POEmployeesIndex_length');
    searchBoxDiv.addClass('m-2');

    //Add bootstrap 5 button css to the Datatables buttons
    var DTJSBtns = $('.dt-button');    
        
    for (var btn of DTJSBtns) {
        btn.classList.remove('dt-button');
        btn.classList.add('btn');
        btn.classList.add('btn-primary');
    }
    
});