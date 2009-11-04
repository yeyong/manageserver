var pageScroller = function() {
    var config = {
        easingType: 'easeOutExpo',
        scrollDuration: 1000,
        scrollTargets: '.article_container'
    };

    function getTargets(scrollTargets) {
        posts = $(scrollTargets);
    }

    function attachScrollEvents($posts, settings) {
        $posts.each(function() {
            $this = $(this);
            // prev link
            $this.find('.prev_next li:not(.next) a').click(function() {
                target = $(this).parents(config.scrollTargets).prev(config.scrollTargets);
                scrollTo(target, config.scrollDuration, config.easingType);
                return false;
            });

            // next link
            $this.find('.prev_next li.next a').click(function() {
                target = $(this).parents(config.scrollTargets).next(config.scrollTargets);
                scrollTo(target, config.scrollDuration, config.easingType);
                return false;
            });
        });
    }

    function scrollTo(target, scrollDuration, easingType) {
        if (target.length > 0) {
            $.scrollTo(target, scrollDuration, { easing: easingType });
        }
    }

    // public methods
    return {
        /*
        * Attach scroll events to the page
        */
        init: function() {
            getTargets(config.scrollTargets);
            attachScrollEvents(posts, config);
        }
    };
}();