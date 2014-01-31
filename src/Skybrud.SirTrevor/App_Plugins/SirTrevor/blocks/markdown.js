/*
  Markdown Block
*/

SirTrevor.Blocks.Markdown = (function () {

    var md_template = _.template([
      '<div class="expanding-textarea">',
        '<span></span><br>',
        '<textarea class="st-markdown st-required"></textarea>',
      '</div>'
    ].join("\n"));

    return SirTrevor.Block.extend({

        type: "Markdown",

        editorHTML: function () {
            return md_template(this);
        },

        loadData: function (data) {
            var cont = this.$('.expanding-textarea'),
                area = cont.find('textarea'),
                span = cont.find('span');
            area.text(data.text);
            span.html(markdown.toHTML(area.val()));

            area.expandingTextarea();
        },

        onBlockRender: function () {
            /* Make our expanding text area */
            var cont = this.$('.expanding-textarea'),
                area = cont.find('textarea'),
                span = cont.find('span');

            area.bind('input', function () {
                span.html(markdown.toHTML(area.val()));
            });
            cont.addClass('active');
        },

        toData: function () {
            var dataObj = {};

            dataObj.text = this.$('.st-markdown').val();
            this.setData(dataObj);
        }

    });

})();

_blocks.push("Markdown");