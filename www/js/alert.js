var alert = {
  error: function(message) {
    this.error(message, 'click');
  },
  error: function(message, closeWith) {
    noty({
      text: message,
      layout: 'top',
      type: 'error',
      closeWith: closeWith,
      timeout: 15000
    });
  }
}
