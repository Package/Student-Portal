
$(function () {

    // Loading overlay on form submissions.
    $('form').submit(function (e) {
        var $this = $(this);
        
        // Form isn't valid, input errors need correcting first.
        if ($this.find('.validation-summary-errors') != undefined) {
            return;
        }

        $('#overlay, #loading').css('display', 'inline-block');
        $this.find('button').addClass('disabled').css('cursor', 'wait');
    });

    // Loading overlay on button clicks.
    $('a').click(function (e) {
        var $this = $(this),
            action = $this.attr('href');

        // List of actions which won't trigger a page re-load.
        var ignored = ['javascript', 'mailto', 'tel', '#']; 

        // If the action isn't going to take you off the current page, don't load the overlay.
        for (var i = 0; i < ignored.length; i++) {
            if (action.includes(ignored[i])) {
                return;
            }
        }
 
        $('#overlay, #loading').css('display', 'inline-block');
        $(this).addClass('disabled').css('cursor', 'wait');
    });

    // When entering qualifications, if the user selects that they don't have any formal quals, 
    // autopopulate the rest of the qualification questions with 'N/A' as they don't need to complete them.
    $('#Type').change(function () {
        var type = $(this).val();

        if (type != undefined && type == 'I don\'t have any qualifications') {
            $('#Name').val('N/A');
            $('#PredictedGrade').val('N/A');
            $('#ActualGrade').val('N/A');
        }
    });
}());