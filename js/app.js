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



function UserAccountModel() {
    this.Username = ko.observable();
    this.UserId - ko.observable();
    this.CreatedDate = ko.observable();
    this.Password = ko.observable();
    this.Email = ko.observable();
    this.TwitterAccount = ko.observable();
    this.phoneNumber = ko.observable();

}

function PostModel() {
    this.UserId = ko.observable();
    this.PostTitle = ko.observable();
    this.PostContent = ko.observable();
    this.PostDate = ko.observable();
}




//Begin View Model
function myViewModel() {
    var self = this;

    //page management
    self.page = ko.observable('show-posts');
    
    //Login State management
    self.login = ko.observable('logged-out')

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
    self.AddAccount = function(){
        self.page('add-account');
        
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