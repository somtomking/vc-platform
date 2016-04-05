﻿angular.module('virtoCommerce.coreModule.seo')
.controller('virtoCommerce.coreModule.seo.seoWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
    var blade = $scope.blade;

    $scope.openSeoBlade = function () {
        if ($scope.widget.skipStoreList) {

            var newBlade = {
                id: 'seoDetails',
                seoContainerObject: $scope.data,
                seoLanguages: $scope.widget.getLanguages(blade),
                updatePermission: blade.updatePermission,
                controller: 'virtoCommerce.coreModule.seo.seoDetailController',
                template: 'Modules/$(VirtoCommerce.Core)/Scripts/SEO/blades/seo-detail.tpl.html'
            };
            bladeNavigationService.showBlade(newBlade, blade);
        } else {
            var newBlade = {
                id: "seoStoreList",
                title: blade.title,
                seoContainerObject: $scope.data,
                seoLanguages: $scope.widget.getLanguages(blade),
                updatePermission: blade.updatePermission,
                controller: 'virtoCommerce.coreModule.seo.storeListController',
                template: 'Modules/$(VirtoCommerce.Core)/Scripts/SEO/blades/store-list.tpl.html'
            };
            bladeNavigationService.showBlade(newBlade, blade);
        }
    };

}]);
