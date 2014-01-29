/*

Hidden for now since I don't want to set the options globally, put per editor. Not sure if possible though.

SirTrevor.setBlockOptions("Tweet", {
    fetchUrl: function (tweetId) {
        return "/base/SkybrudSirTrevor/GetTweet/" + tweetId;
    }
});
*/
SirTrevor.setDefaults({
    uploadUrl: "/App_Plugins/SirTrevor/Upload/Image"
});
SirTrevor.EventBus.on('block:create:new', function (data) {
    if (data && data.el) {
        var $el = jQuery(data.el);
        jQuery('.tabpagescrollinglayer').scrollTo($el, 400, { offset: { top: -$el.height(), left: 0 } });
    }
});
jQuery(function(){
    jQuery('textarea.sir-trevor-editor').each(function (index, element) {
        var $element = jQuery(element);
        var editor = new SirTrevor.Editor({ el: $element, blockTypes: enabledBlockTypes() });
        editor.onFormSubmit();
        editor.$el.val( JSON.stringify(editor.dataStore, null, '  ') );
    });
});