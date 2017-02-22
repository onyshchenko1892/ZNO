var IndexPage = function() {
    var that = this;

    this.init = function() {
        $("#show-menu-btn").on("click",function(){
            $(".menu").show("slow");
        });
        $("#hide-menu-btn").on("click",function(){
            $(".menu").hide(500);
        });
    };
};

$(function() {
    var indexPage = new IndexPage();
    indexPage.init();
});
