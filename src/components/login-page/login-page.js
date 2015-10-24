import ko from 'knockout';
import templateMarkup from 'text!./login-page.html';

class LoginPage {
    constructor(params) {
        this.message = ko.observable('Hello from the login-page component!');
    }
    
    dispose() {
        // This runs when the component is torn down. Put here any logic necessary to clean up,
        // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
    }
}

export default { viewModel: LoginPage, template: templateMarkup };
