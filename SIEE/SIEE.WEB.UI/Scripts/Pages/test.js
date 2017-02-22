var TestPage = function () {
    var that = this;
    this.testId = 2200611;//parseInt(window.location.href.split("/").pop());
    this.test = new Test(this.testId);

    this.init = function () {
        $('#prev-question').on('click', that.prevQuestionClick);
        $('#next-question').on('click', that.nextQuestionClick);
        $('.end-test button').on('click', that.endTestClick);
        $('.start-test .timed').on('click', { withTime: true }, that.startTestClick);
        $('.start-test .no-timed').on('click', { withTime: false }, that.startTestClick);
        that.test.loadTest(
            function () {
                $(".start-test button")
                    .removeClass("disabled")
                    .addClass("enabled");
            });
    };

    /*EVENT HANDLERS*/
    this.startTestClick = function (event) {
        if (that.test.questions.length === 0) {
            alert("Неможливо розпочати тест");
            return;
        } else {
            if (!event.data.withTime) {
                that.prepareUIForTest();
                that.test.startTest();
                var question = that.test.getQuestionByQuestionNumber(1);
                if (question != undefined) {
                    that.showQuestion(question, true);
                } else {
                    alert("Неможливо розпочати тест");
                }
            }
        }
    };

    this.questionMapItemClick = function (event) {
        var question = that.test.getQuestionByQuestionNumber(parseInt($($(event.target)[0]).text()));
        that.showQuestion(question);
    };

    this.endTestClick = function () {
        $(".question").remove();
        for (var i = 0; i < that.test.getQuestionNumbers() ; i++) {
            that.showQuestion(that.test.questions[i], false, true);
        }
        var userResult = that.test.endTest();
        that.showUserResult(userResult["numberOfTrueAnswer"], userResult["numberOfFalseAnswer"], userResult["testMark"], userResult["maxTestMark"]);
    };

    this.prevQuestionClick = function () {
        if (that.test.getNumberOfCurrentQuestion() === 1) {
            return;
        }
        that.showQuestion(that.test.getQuestionByQuestionNumber(that.test.getNumberOfCurrentQuestion() - 1), true);
        $("#currentQuestion").first().text(that.test.getNumberOfCurrentQuestion());
    };

    this.nextQuestionClick = function () {
        if (that.test.getNumberOfCurrentQuestion() === that.test.getQuestionNumbers()) {
            return;
        }
        that.showQuestion(that.test.getQuestionByQuestionNumber(that.test.getNumberOfCurrentQuestion() + 1), true);
        $("#currentQuestion").first().text(that.test.getNumberOfCurrentQuestion());
    };

    this.markedListItemClick = function (event) {
        switch ($(".question").attr("type")) {
            case "RadioQuestion":
                that.markRadioAnswer(event.currentTarget);
                break;
            case "TextImageQuestionImageAnswers":
                that.markRadioAnswer(event.currentTarget);
                break;
        }
    };

    this.selfAnswerChanged = function () {
        that.test.setAnswer(that.test.getNumberOfCurrentQuestion(), $("input[name=answer]").first().val());
        if (!$(".current-question").hasClass("answered")) {
            $(".current-question").addClass("answered");
        }
    };
    /*/EVENT HANDLERS*/

    this.prepareUIForTest = function () {
        $('body').css('background-color', '#fff');
        $('.content').addClass('content-bordered');
        $('.manage-btns').addClass('manage-btns-bordered');
        $('.start-test').hide();
        $('.end-test').show();
        $('.navigate-questions').show();
        $('#Questionnumbers').text(that.test.getQuestionNumbers());
        that.initQuestionMapBlock();
    };

    this.initQuestionMapBlock = function () {
        $('.questions-map').show();
        for (var i = 0; i < that.test.getQuestionNumbers() ; i++) {
            var a = document.createElement("a");
            $(a)
                .text(i + 1)
                .on('click', that.questionMapItemClick);
            $('.questions-map').append(a);
        }
    };

    this.showUserResult = function (numberOfTrueAnswer, numberOfFalseAnswer, testMark, maxTestMark) {
        var questionNumbers = numberOfTrueAnswer + numberOfFalseAnswer;
        $(".manage-btns").hide();
        $(".questions-map").hide();
        $(".user-result").show();
        $(".progress-bar-success").css("width", ((100 / (questionNumbers)) * numberOfTrueAnswer) + "%")
            .text(Math.round((100 / (questionNumbers)) * numberOfTrueAnswer) + "%");
        $(".progress-bar-danger").css("width", ((100 / (questionNumbers)) * numberOfFalseAnswer) + "%")
            .text(Math.round((100 / (questionNumbers)) * numberOfFalseAnswer) + "%");
        $(".number-true-answers").text(numberOfTrueAnswer);
        $(".number-false-answers").text(numberOfFalseAnswer);
        $("#test-mark").text(testMark);
        $("#max-test-mark").text(maxTestMark);
    };

    this.showQuestion = function (question, clearPlaceForQuestion, showTrueAnswer) {
        this.updateNavigateQuestionsBtns(question["QuestionNumber"]); //"enable" or "disable" prev/next btns
        this.updateQuestionsMap(question["QuestionNumber"]); //show currentQuestion on page
        this.test.setNumberOfCurrentQuestion(question["QuestionNumber"]);
        switch (question["Type"]) {
            case "RadioQuestion":
                var radioQuestion = new RadioQuestion();
                radioQuestion.init(question, this, this.test.getAnswer(question["QuestionNumber"]), clearPlaceForQuestion, showTrueAnswer);
                break;
            case "SelfAnswer":
                var selfAnswer = new SelfAnswer();
                selfAnswer.init(question, this, this.test.getAnswer(question["QuestionNumber"]), clearPlaceForQuestion, showTrueAnswer);
                break;
        };
        window.MathJax.Hub.Queue(["Typeset", MathJax.Hub]);
    };

    this.updateNavigateQuestionsBtns = function (questionNumber) {
        if (questionNumber === this.test.getQuestionNumbers()) {
            $("#next-question").removeClass("enabled");
        } else {
            if (!$("#next-question").hasClass("enabled")) {
                $("#next-question").addClass("enabled");
            }
        }
        if (questionNumber === 1) {
            $("#prev-question").removeClass("enabled");
        } else {
            if (!$("#prev-question").hasClass("enabled")) {
                $("#prev-question").addClass("enabled");
            }
        }
    };

    this.updateQuestionsMap = function (currentQuestion) {
        $(".questions-map a").removeClass("current-question");
        var arrOfLinks = $(".questions-map a");
        $(arrOfLinks[currentQuestion - 1]).addClass("current-question");
        $("#currentQuestion").text(currentQuestion);
    }

    this.markRadioAnswer = function (element) {
        if (that.test.isTestRunning) {
            $(element).parent().children(".marked-list-item").removeClass("marked-list-item-selected");
            $(element).addClass("marked-list-item-selected");
            that.test.setAnswer(that.test.getNumberOfCurrentQuestion(), $(element).attr("answer"));
            if (!$(".current-question").hasClass("answered")) {
                $(".current-question").addClass("answered");
            }
        }
    };


};

var Test = function (testId) {
    var that = this;
    this.questions = []; //array of test questionss
    this.userAnswers = []; //array of user answers to thequestions
    this.numberOfCurrentQuestion = undefined;
    this.isTestRunning = undefined;
    this.testId = testId;

    this.startTest = function () {
        this.isTestRunning = true;
        this.sortQuestions();
    };

    this.endTest = function () {
        this.isTestRunning = false;
        var numberOfTrueAnswer = 0;
        var numberOfFalseAnswer = 0;
        var testMark = 0;
        var sumTestMarks = 0;
        for (var i = 0; i < this.questions.length; i++) {
            if (this.userAnswers[i] === this.questions[i]["TrueAnswer"]) { //userAnswers + 1 при присвоєнні
                numberOfTrueAnswer++;
                testMark += this.questions[i]["QuestionPrice"];
            } else {
                numberOfFalseAnswer++;
            }
            sumTestMarks += this.questions[i]["QuestionPrice"];
        }
        return { "numberOfTrueAnswer": numberOfTrueAnswer, "numberOfFalseAnswer": numberOfFalseAnswer, "testMark": testMark, "maxTestMark": sumTestMarks };
    };

    this.loadTest = function (callback) {
        var xhr = $.ajax({
            url: "/Test/GetTestQuestionsById",
            data: { testId: that.testId },
            type: "GET",
            success: function (data) {
                that.questions = data;
            },
            error: function () {
                console.log("Error" + that.testId);
                console.log("Error" + (typeof that.testId));
            }
        });

        xhr.done(function () { callback() });
    };

    this.sortQuestions = function () {
        this.questions = this.questions.sort(
            function (q1, q2) {
                if (q1["QuestionNumber"] < q2["QuestionNumber"])
                    return -1;
                if (q1["QuestionNumber"] > q2["QuestionNumber"])
                    return 1;
                return 0;
            });
    };

    this.getQuestionByQuestionNumber = function (questionNumber) {
        if (!this.questions.some(q => q["QuestionNumber"] === questionNumber)) {
            return undefined;
        } else {
            return this.questions.find(q => q["QuestionNumber"] === questionNumber);
        }
    };

    this.setAnswer = function (questionNumber, answer) {
        this.userAnswers[questionNumber - 1] = answer;
    };

    this.getAnswer = function (questionNumber) {
        return this.userAnswers[questionNumber - 1];
    };

    this.getQuestionNumbers = function () {
        return this.questions.length;
    };

    this.getNumberOfCurrentQuestion = function () {
        return this.numberOfCurrentQuestion;
    };

    this.setNumberOfCurrentQuestion = function (numberOfCurrentQuestion) {
        this.numberOfCurrentQuestion = numberOfCurrentQuestion;
    };
};

var Question = function (question, clearPlaceForQuestion) {

    var that = this;
    this.question = question;
    this.clearPlaceForQuestion = clearPlaceForQuestion;

    this.init = function () {
        if (clearPlaceForQuestion || clearPlaceForQuestion === undefined) {
            this.clearPlace();
        }
        that.createQuestionSentenceBlock();

    };

    this.createQuestionSentenceBlock = function () {
        var divQuestion = document.createElement("div");
        $(divQuestion).addClass("question").attr("type", this.question["Type"]).attr("Questionnumber", this.question["QuestionNumber"]);

        var divQuestionSentence = document.createElement("div");
        $(divQuestionSentence).addClass("row question-sentence");

        var div = document.createElement("div");
        $(div).html(this.question["QuestionSentence"]);

        $(divQuestionSentence).append(div);
        $(divQuestion).append(divQuestionSentence);
        $(".content").append(divQuestion);
    };

    this.createQuestionImageBlock = function () {
        var divQuestionImage = document.createElement("div");
        $(divQuestionImage).addClass("question-image col-sm-4");
        var img = document.createElement("img");
        $(img).addClass("img-responsive").attr("src", this.question["ImagePath"]);
        $(divQuestionImage).append(img);
        $(".question-answers > .row").append(divQuestionImage);
    };

    this.clearPlace = function () {
        $(".question").remove();
    };

    this.init();
};

var RadioQuestion = function () {

    this.testPage = undefined;

    this.init = function (question, testPage, userAnswer, clearPlaceForQuestion, showTrueAnswer) {
        Question.call(this, question, clearPlaceForQuestion);
        this.testPage = testPage;

        if (question["ImagePath"]) {
            this.createAnswersBlock(question, "col-sm-8", showTrueAnswer);
        } else {
            if (question["A"].indexOf('.png') === -1) {
                this.createAnswersBlock(question, "col-sm-12", showTrueAnswer);
            } else {
                this.createAnswersBlock(question, "col-sm-4", showTrueAnswer);
            }
        }
        this.showUserAnswer(userAnswer, question["QuestionNumber"]);
    };

    this.showUserAnswer = function (userAnswer, questionNumber) {
        if (userAnswer) {
            this.testPage.markRadioAnswer($(".question[questionnumber=" + questionNumber + "] div.marked-list-item[answer=" + userAnswer + "]"));
        }
    };

    this.createAnswersBlock = function (question, divMarkedCLass, showTrueAnswer) {
        var answers = [question["A"], question["B"], question["C"], question["D"], question["E"]];
        var answersLettters = ["А", "Б", "В", "Г", "Д"].reverse();

        var divQuestionAnswers = document.createElement("div");
        $(divQuestionAnswers).addClass("question-answers");

        var divRow = document.createElement("div");
        $(divRow).addClass("row");

        for (var i = 0; i < answers.length; i++) {
            if (answers[i] !== "") {
                var answerLetter = answersLettters.pop();
                var divMarkedListItem = document.createElement("div");
                if (showTrueAnswer && (answerLetter === question["TrueAnswer"])) {
                    $(divMarkedListItem).addClass(divMarkedCLass + " marked-list-item true");
                } else {
                    $(divMarkedListItem).addClass(divMarkedCLass + " marked-list-item");
                }
                $(divMarkedListItem).attr({ answer: answerLetter });
                var span = document.createElement("span");
                $(span).addClass("text-center");
                $(span).text(answerLetter);
                var div = document.createElement("div");
                if (answers[i].indexOf('.png') === -1) {
                    $(div).html(answers[i]);
                } else {
                    var img = document.createElement("img");
                    $(img).addClass("img-responsive").attr("src", "/Images/Tests/" + question["TestId"] + "/" + answers[i]);
                    $(div).append(img);
                }
                $(divMarkedListItem).append(span).append(div).on("click", { answer: answerLetter }, this.testPage["markedListItemClick"]);
                $(divRow).append(divMarkedListItem);
            }
        }
        $(".question").last().append(divRow);
    };
};

var SelfAnswer = function () {
    this.testPage = undefined;

    this.init = function (question, testPage, userAnswer, clearPlaceForQuestion, showTrueAnswer) {
        Question.call(this, question, clearPlaceForQuestion);
        this.testPage = testPage;
        this.createAnswerBlock(question, showTrueAnswer, userAnswer);
    };

    this.createAnswerBlock = function (question, showTrueAnswer, userAnswer) {
        var divQuestionAnswers = document.createElement("div");
        $(divQuestionAnswers).addClass("question-answers");

        var divRow = document.createElement("div");
        $(divRow).addClass("row");

        var divSelfAnswer = document.createElement("div");
        if (question["ImagePath"]) {
            var divQuestionImage = document.createElement("div");
            $(divQuestionImage).addClass("col-sm-6 question-image");
            var img = document.createElement("img");
            $(img).addClass("img-responsive").attr("src", "/Images/Tests/" + question["TestId"] + "/" + question["ImagePath"]);
            $(divQuestionImage).append(img);
            $(divRow).append(divQuestionImage);
            $(divSelfAnswer).addClass("col-sm-6 self-answer");
        } else {
            $(divSelfAnswer).addClass("col-sm-12 self-answer");
        }

        var labelAnswer = document.createElement("label");
        $(labelAnswer).attr("for", "answer").text("Ваша відповідь: ");
        $(divSelfAnswer).append(labelAnswer);
        var input = document.createElement("input");
        $(input).attr({ name: "answer" }, { type: "number" })
            .on("change", this.testPage.selfAnswerChanged)
            .val(userAnswer);
        if (showTrueAnswer) {
            $(input).attr('disabled', 'disabled');
            this.showTrueAnswer(userAnswer, question, input, divSelfAnswer);
        } else {
            $(divSelfAnswer).append(input);
        }
        $(divRow).append(divSelfAnswer);
        $(".question").last().append(divRow);
    };

    this.showTrueAnswer = function (userAnswer, question, input, divSelfAnswer) {
        var div = document.createElement("div");
        if (userAnswer === question["TrueAnswer"]) {
            $(input).addClass('correct');
        } else {
            $(input).addClass('incorrect');
            $(div).html("Правильна відповідь: " + question["TrueAnswer"]);
        }
        $(divSelfAnswer).append(input).append(div);
    };
};

$(function () {
    var testPage = new TestPage();
    testPage.init();
    /*MathJax.Ajax.Require("[MathJax]/config/myConfig.js",function () {
        alert("My configuration file is loaded");
    });*/
    window.MathJax.Hub.Config({ tex2jax: { inlineMath: [['$', '$'], ['\\(', '\\)']], processEscapes: false } });
});
