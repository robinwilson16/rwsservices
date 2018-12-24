$(function () {
    return $(".carousel.lazy").on("slide.bs.carousel", function (ev) {
        var lazy;
        lazy = $(ev.relatedTarget).find("img[data-src]");
        lazy.attr("src", lazy.data('src'));
        lazy.removeAttr("data-src");
    });
});

$(".scrollAnchor a").click(function (e) {
    e.preventDefault();

    var pageRef = $(this).attr("href");

    if (pageRef[0] != '#') {
        window.location.href = pageRef;
    } else {
        var pageRefPosition = $(pageRef).offset().top;

        $("html,body").animate({
            scrollTop: pageRefPosition
        }, 400);
    }
});

//Push notifications
function initialiseState() {
    var pushButton = document.querySelector('.GetNotificationsButton');
    if (!('showNotification' in ServiceWorkerRegistration.prototype)) {
        pushButton.disabled = true;
        console.warn('Notifications aren\'t supported.');
        return;
    }
    else if (Notification.permission === 'denied') {
        pushButton.disabled = true;
        console.warn('The user has blocked notifications.');
        return;
    }

    if (!('PushManager' in window)) {
        pushButton.disabled = true;
        console.warn('Push messaging isn\'t supported.');
        return;
    }

    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription()
            .then(function (subscription) {
                pushButton.checked = true;
                if (!subscription) {
                    pushButton.checked = false;
                    return;
                }

                // Keep your server in sync with the latest subscriptionId
                //TODO
                //sendSubscriptionToServer(subscription);
            })
            .catch(function (err) {
                console.warn('Error during getSubscription()', err);
            });
    });
}
initialiseState();

function subscribeUser() {
    var pushButton = document.querySelector('.GetNotificationsButton');
    return new Promise(function (resolve, reject) {
        const permissionResult = Notification.requestPermission(function (result) {
            resolve(result);
        });

        if (permissionResult) {
            permissionResult.then(resolve, reject);
        }
    })
        .then(function (permissionResult) {
            if (permissionResult !== 'granted') {
                pushButton.checked = false;
                throw new Error('Notification permission was not granted.');
            }
            else {
                processSubscription();
            }
        });
}

function unsubscribeUser() {
    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription()
            .then(function (subscription) {
                subscription.unsubscribe().then(function (successful) {
                    alert("You have been successfully unsubscribed and will recieve no further notifications from this site.");
                }).catch(function (e) {
                    alert("Sorry, there was an error removing your subscription.\nClick to the left of the address bar if you wish to block notications from this site.");
                    pushButton.checked = true;
                });
            })
            .catch(function (err) {
                console.error('Error during getSubscription()', err);
            });
    });
}

$(".GetNotificationsButton").click(function (e) {
    var isBlocked = $(this).is(':disabled');
    var getNotifications = $(this).is(':checked');

    if (getNotifications) {
        subscribeUser();
    }
    else {
        unsubscribeUser();
    }
});

$(".GetNotifications").click(function (e) {
    var isBlocked = $(".GetNotificationsButton").is(':disabled');
    var getNotifications = $(this).is(':checked');

    if (isBlocked) {
        alert("You previously blocked this website from showing notifications.\nClick to the left of the address bar to re-enable.");
    }
});

function processSubscription() {
    navigator.serviceWorker.ready
        .then(function (registration) {
            const subscribeOptions = {
                userVisibleOnly: true,
                applicationServerKey: urlBase64ToUint8Array(
                    'BNcoqSRgSD_ASSk4PVqR91tJ0SdVUkW3u3mhEkGiAAV5YLEr98pQkUs9afu6KJAOteGWL9tNNE4okoWi8vUQRG0'
                )
            };

            return registration.pushManager.subscribe(subscribeOptions);
        })
        .then(function (pushSubscription) {
            //TODO
            //sendSubscriptionToServer(subscription);
            console.log('Received PushSubscription: ', JSON.stringify(pushSubscription));
            return pushSubscription;
        });
}

function urlBase64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}