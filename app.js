//ko.validation.rules.pattern.message = 'Invalid.';
//
//ko.validation.init({
//    registerExtenders: true,
//    messagesOnModified: true,
//    insertMessages: true,
//    parseInputAttributes: true,
//    messageTemplate: null
//}, true);
//





function UserModel() {
    this.Username = ko.observable();
    this.UserId - ko.observable();
    this.CreatedDate = ko.observable();
    this.Password = ko.observable();
    this.EmailAddress = ko.observable();
    this.TwitterHandle = ko.observable();
    this.PhoneNumber = ko.observable();

}

function PostModel() {
    this.UserId = ko.observable();
    this.PostId = ko.observable();
    this.PostTitle = ko.observable();
    this.PostContent = ko.observable();
    this.PostDate = ko.observable();
}




//Begin View Model
function myViewModel() {
     var self = this;
    var Code = self.GetSetRandomNumber();
   

    //page management
    self.page = ko.observable('show.posts');

    //Login State management
    self.login = ko.observable('logged.out')

    //Blog Posts
    self.posts = ko.observableArray();


    //UserInformation
    self.ContactPhone = ko.observable().extend({
        required: true,
        pattern: {
            message: 'Invalid phone number.',
            params: /^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$/
        }
    });

    self.emailAddress = ko.observable().extend({
        // custom message
        required: {
            message: 'Please supply your email address.'
        }
    });

    self.twitterAccount = ko.observable();

    self.captcha = ko.observable().extend({
        // custom validator
        validation: {
            validator: captcha,
            message: 'Please check Captcha.'
        }

    });



    //Page navigaiton
    self.AddAccount = function () {
        self.page('account.add');
    }

    self.Home = function () {
        self.page('show.posts');
    }

    self.LoggedIn = function () {
        self.login('logged-in');
    }



    self.GetSetRandomNumber = function () {
        var RandomNumber = Math.floor(Math.random() * 90000) + 10000;
        return RandomNumber;
    }




    self.getPhoneNUmber = function () {
        var PhoneNumber = $('#InputPhoneNumber').val();
        return PhoneNumber;

    }

    
    self.ShowAllPosts = function(){
        $.ajax({
            type: 'GET',
            url: 'http://localhost:56499/api/posts/',
            success: function(data){
                ko.mapping.fromJS(data, {}, self.posts())
            }
        });
    }




    //
    //    self.submit = function () {
    //        if (viewModel.errors().length === 0) {
    //            alert('Thank you.');
    //        } else {
    //            alert('Please check your submission.');
    //            viewModel.errors.showAllMessages();
    //        }
    //    };
    //    self.confirmPassword = ko.observable().extend({
    //        validation: {
    //            validator: mustEqual,
    //            message: 'Passwords do not match.',
    //            params: viewModel.password
    //        }
    //    });
    //
    var captcha = function (val) {
        return val == 33;
    };

    var mustEqual = function (val, other) {
        return val == other;
    };



} //End ViewModel







//
//viewModel.errors = ko.validation.group(viewModel);
//
//viewModel.requireLocation = function () {
//    viewModel.location.extend({
//        required: true
//    });
//};

ko.applyBindings(new myViewModel());









//jQuery page Flow 
$(document).ready(function () {

    //    
    //    //Pages
    //    var AccountsPage = $('.CreateAccount');
    //    var HomePageHeader = $('.HomePageHeader');
    //    var HomePageGrid = $('.HomePageGrid');
    //    
    //    //Buttons
    //    var LeavePostBtn = $('#leavePostBtn');
    //    var LinkTwitterBtn = $('.LinkTwitterBtn');
    //    var AddAccountBtn = $('.AddAccountBtn');
    //    
    //    
    //    
    //    
    //    
    //    //Page Start
    //    $(LeavePostBtn).hide();
    //    $(AccountsPage).hide();
    //    $(LinkTwitterBtn).hide();
    //    
    //    
    //    //Create an Account Button Click
    //    $(AddAccountBtn).on("click", function(){
    //        alert("test");
    //      
    //        $(HomePageHeader).fadeOut();
    //        $(HomePageGrid).fadeOut();
    //        $(AccountsPage).show();
    //      $('.CreateAccount').fadeIn();
    //        $('.createAccountArea').fadeIn();

});