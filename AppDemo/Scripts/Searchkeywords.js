
var dataset = [];
var fieldtocheck = "";


//jQuery(document).on('keypress', "input[placeholder='Enter Keywords']", function (objEvent) {
//    if (objEvent.keyCode == 9) {

//        objEvent.preventDefault(); // stops its action
//    }
//    else if (objEvent.keyCode == 32) {
//        objEvent.preventDefault()
//    }
//});


jQuery("body").on("keyup", "input[placeholder^='Enter Keywords']", function () {

    jQuery(".mhgt").parent().hide();

    var field = jQuery(this);
    fieldtocheck = field.val();
    //setTimeout(function() {

    if (field.val().length >= 3) {

        jQuery(".pro-bar").show();
        var param = {};
        fieldtocheck = field.val();

        param.keywords = field.val();
        searchKeyword = field.val();
        jQuery.post("/Home/GetKeywords", param, function (data) {
            if (data != "0" && data.length > 0) {
                jQuery(".mhgt").html('');

                jQuery.each(data, function (val, key) {
                    if (key.indexOf("[Vendor]") == "-1") {
                        dataset.push(key);
                        var li = '<li><a href="javascript:void(0);" onclick="selectKeyword(this)"; data-val="' + param.keywords + '">' + key + '</a></li>';
                        jQuery(".mhgt").append(li);
                    }

                })
                jQuery(".mhgt").parent().show();
            }

            jQuery(".pro-bar").hide();
        });
        return false;
    }
    //}, 300);
});
jQuery("body").click(function () {

    if (fieldtocheck.length > 0) {

        if (jQuery.inArray(fieldtocheck, dataset) == -1) {


            jQuery('#txtKeywordsadd').tagsinput('remove', fieldtocheck);
            jQuery('#txtKeywordsadd').tagsinput('refresh');
            fieldtocheck = "";
        }
    }
    if (jQuery(".mhgt").parent().is(":visible")) {
        jQuery(".mhgt").parent().hide();


    }
})


function selectKeyword(ctrl) {

    setTimeout(function () {
        var keyword = jQuery(ctrl).text();
        var searchedKeyword = jQuery(ctrl).attr("data-val");//searchKeyword;//
        jQuery('#txtKeywordsadd').tagsinput('remove', searchedKeyword);
        // jQuery('#txtKywordread').tagsinput('remove', searchedKeyword);
        jQuery('#txtKeywordsadd').tagsinput('refresh');
        keyword = keyword.replace(/&amp;/g, "&");
        jQuery("#txtKeywordsadd").tagsinput('add', keyword);
        //   jQuery('#txtKywordread').tagsinput('add', keyword);
        // jQuery('#txtKeywordsadd').tagsinput('refresh');
        jQuery(".mhgt").parent().hide();
        fieldtocheck = "";
    }, 300);

}
/**
 * @description determine if an array contains one or more items from another array.
 * @param {array} haystack the array to search.
 * @param {array} arr the array providing items to check for in the haystack.
 * @return {boolean} true|false if haystack contains at least one item from arr.
 */
var findOne = function (haystack, arr) {
    return arr.some(function (v) {
        return haystack.indexOf(v) >= 0;
    });
};