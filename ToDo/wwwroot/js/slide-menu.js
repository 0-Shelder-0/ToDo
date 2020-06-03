$(function () {
    $('.panel').tabSlideOut({
        tabHandle: '.handle',                       //КЛАСС КНОПКИ
        pathToTabImage: 'button2.png',               //ПУТЬ К ИЗОБРАЖЕНИЮ КНОПКИ
        imageHeight: '50px',                        //ВЫСОТА КНОПКИ
        imageWidth: '50px',                         //ШИРИНА КНОПКИ
        tabLocation: 'right',                       //РАСПОЛОЖЕНИЕ ПАНЕЛИ TOP - ВЫДВИГАЕТСЯ СВЕРХУ, RIGHT - ВЫДВИГАЕТСЯ СПРАВА, BOTTOM - ВЫДВИГАЕТСЯ СНИЗУ, LEFT - ВЫДВИГАЕТСЯ СЛЕВА
        speed: 300,                                 //СКОРОСТЬ АНИМАЦИИ
        action: 'click',                            //МЕТОД ПОКАЗА CLICK - ВЫДВИГАЕТСЯ ПО КЛИКУ НА КНОПКУ, HOVER - ВЫДВИГАЕТСЯ ПРИ НАВЕДЕНИИ КУРСОРА
        topPos: '75px',                             //ОТСТУП СВЕРХУ
        fixedPosition: false                        //ПОЗИЦИОНИРОВАНИЕ БЛОКА FALSE - POSITION: ABSOLUTE, TRUE - POSITION: FIXED
    });
});
(function ($) {
    $.fn.tabSlideOut = function (callerSettings) {
        var settings = $.extend({}, callerSettings || {});

        settings.tabHandle = $(settings.tabHandle);
        var obj = this;
        if (settings.fixedPosition === true) {
            settings.positioning = 'fixed';
        } else {
            settings.positioning = 'absolute';
        }

        //IE6 DOESN'T DO WELL WITH THE FIXED OPTION
        if (document.all && !window.opera && !window.XMLHttpRequest) {
            settings.positioning = 'absolute';
        }

        //SET INITIAL TABHANDLE CSS
        settings.tabHandle.css({
            'display': 'block',
            'width': settings.imageWidth,
            'height': settings.imageHeight,
            'textIndent': '-99999px',
            'background': 'url(' + settings.pathToTabImage + ') no-repeat',
            'outline': 'none',
            'position': 'absolute'
        });

        obj.css({
            'line-height': '1',
            'position': settings.positioning
        });


        var properties = {
            containerWidth: parseInt(obj.outerWidth(), 10) + 'px',
            containerHeight: parseInt(obj.outerHeight(), 10) + 'px',
            tabWidth: parseInt(settings.tabHandle.outerWidth(), 10) + 'px',
            tabHeight: parseInt(settings.tabHandle.outerHeight(), 10) + 'px'
        };

        //SET CALCULATED CSS
        if (settings.tabLocation === 'top' || settings.tabLocation === 'bottom') {
            obj.css({'left': settings.leftPos});
            settings.tabHandle.css({'right': 0});
        }

        if (settings.tabLocation === 'top') {
            obj.css({'top': '-' + properties.containerHeight});
            settings.tabHandle.css({'bottom': '-' + properties.tabHeight});
        }

        if (settings.tabLocation === 'bottom') {
            obj.css({'bottom': '-' + properties.containerHeight, 'position': 'fixed'});
            settings.tabHandle.css({'top': '-' + properties.tabHeight});

        }

        if (settings.tabLocation === 'left' || settings.tabLocation === 'right') {
            obj.css({
                'height': properties.containerHeight,
                'top': settings.topPos
            });

            settings.tabHandle.css({'top': 0});
        }

        if (settings.tabLocation === 'left') {
            obj.css({'left': '-' + properties.containerWidth});
            settings.tabHandle.css({'right': '-' + properties.tabWidth});
        }

        if (settings.tabLocation === 'right') {
            obj.css({'right': '-' + properties.containerWidth});
            settings.tabHandle.css({'left': '-' + properties.tabWidth});

            $('html').css('overflow-x', 'hidden');
        }

        //FUNCTIONS FOR ANIMATION EVENTS

        settings.tabHandle.click(function (event) {
            event.preventDefault();
        });

        var slideIn = function () {

            if (settings.tabLocation === 'top') {
                obj.animate({top: '-' + properties.containerHeight}, settings.speed).removeClass('open');
            } else if (settings.tabLocation === 'left') {
                obj.animate({left: '-' + properties.containerWidth}, settings.speed).removeClass('open');
            } else if (settings.tabLocation === 'right') {
                obj.animate({right: '-' + properties.containerWidth}, settings.speed).removeClass('open');
            } else if (settings.tabLocation === 'bottom') {
                obj.animate({bottom: '-' + properties.containerHeight}, settings.speed).removeClass('open');
            }

        };

        var slideOut = function () {

            if (settings.tabLocation == 'top') {
                obj.animate({top: '-3px'}, settings.speed).addClass('open');
            } else if (settings.tabLocation == 'left') {
                obj.animate({left: '-3px'}, settings.speed).addClass('open');
            } else if (settings.tabLocation == 'right') {
                obj.animate({right: '-3px'}, settings.speed).addClass('open');
            } else if (settings.tabLocation == 'bottom') {
                obj.animate({bottom: '-3px'}, settings.speed).addClass('open');
            }
        };

        var clickScreenToClose = function () {
            obj.click(function (event) {
                event.stopPropagation();
            });

            $(document).click(function () {
                slideIn();
            });
        };

        var clickAction = function () {
            settings.tabHandle.click(function (event) {
                if (obj.hasClass('open')) {
                    slideIn();
                } else {
                    slideOut();
                }
            });

            clickScreenToClose();
        };

        var hoverAction = function () {
            obj.hover(
                function () {
                    slideOut();
                },

                function () {
                    slideIn();
                });

            settings.tabHandle.click(function (event) {
                if (obj.hasClass('open')) {
                    slideIn();
                }
            });
            clickScreenToClose();

        };

        //CHOOSE WHICH TYPE OF ACTION TO BIND
        if (settings.action === 'click') {
            clickAction();
        }

        if (settings.action === 'hover') {
            hoverAction();
        }
    };
})(jQuery);