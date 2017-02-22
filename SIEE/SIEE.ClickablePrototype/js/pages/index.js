var IndexPage = function() {

    var that = this;

    this.init = function() {
        $("#show-menu-btn").on("click",function(){
            $(".menu").show("slow");
        });
        $(".show-sign-in-block").on("click",function(){
           
            $(".menu").show();
            $(".menu .show-sign-in-block").hide();
            $(".sign-in").show();
        });
        $("#show-sign-up-block").on("click",function(){
            $(".sign-up").show();
            $(".features-activities").hide();
        });
        $("#hide-menu-btn").on("click",function(){
            $(".menu").hide(500);
        });
    };

    this.signUpBtnClick = function() {
        $(".sign-up-btn").css("display", "none");
        $(".app-btn").css("display", "none");
        $(".sign-up form").css("display", "block");

    };
};

$(function() {
    var indexPage = new IndexPage();
    indexPage.init();
});
