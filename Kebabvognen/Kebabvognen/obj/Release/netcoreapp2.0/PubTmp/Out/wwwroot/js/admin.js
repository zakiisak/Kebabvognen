var app = angular.module('myapp', []);
app.controller('appctrl', function($scope, $timeout) {
  $scope.name = 'Yuval';
  $scope.showVal = function() {alert($scope.name);};
  $scope.onValueChanged = function(val, done) {
    $timeout(function() {
      var err = Math.random() > 0.5 ? new Error() : null; // Lets fail somtimes
      done(err);
    }, 1000);
  }
});

function EditLabelController() {
  this.mode = EditLabelController.Modes.View;
  this.originalValue = '';
}

EditLabelController.Modes = {
  View: 'View',
  Edit: 'Edit',
  Updating: 'Updating'
};

EditLabelController.prototype.$onChanges = function(changes) {
  if (changes && typeof changes.value !== 'undefined') {
    this.originalValue = this.value;
  }
};

EditLabelController.prototype.onEdit = function() {
  this.mode = EditLabelController.Modes.Edit;
};

EditLabelController.prototype.onUpdate = function() {
  if (typeof this.valueChanged === 'function') {
    this.mode = EditLabelController.Modes.Updating;
    this.valueChanged({value: this.getUpdatedValue(), done: this.onUpdateDone.bind(this)});
  } else {
    this.mode = EditLabelController.Modes.View;
  }
};

EditLabelController.prototype.onUpdateDone = function(err) {
  this.mode = EditLabelController.Modes.View;
  if (err) {
    this.value = this.originalValue;
  } else {
    this.originalValue = this.value;
  }
};

EditLabelController.prototype.getUpdatedValue = function() {
  return this.value;
};

EditLabelController.prototype.cancelEdit = function() {
  this.mode = EditLabelController.Modes.View;
  this.value = this.originalValue;
};

app.component('editLabel', {
  templateUrl: 'edit-label.html',
  controller: EditLabelController,
  bindings: {
    value: '<',
    valueChanged: '&?'
  }
});