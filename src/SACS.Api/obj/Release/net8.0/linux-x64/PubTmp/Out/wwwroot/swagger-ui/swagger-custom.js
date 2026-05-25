(function () {
  // Enhanced auto-token injector for Swagger UI
  console && console.log && console.log('[swagger-custom] attempting to fetch token from /auth/token');

  fetch('/auth/token', { method: 'POST' })
    .then(function (r) {
      if (!r.ok) throw new Error('status ' + r.status);
      return r.json();
    })
    .then(function (json) {
      if (!json || !json.access_token) {
        console && console.warn && console.warn('[swagger-custom] token response missing access_token', json);
        return;
      }
      var bearer = 'Bearer ' + json.access_token;
      console && console.log && console.log('[swagger-custom] obtained token:', bearer);

      function tryPreauthorize(ui) {
        try {
          if (ui && typeof ui.preauthorizeApiKey === 'function') {
            // Try to preauthorize using the security scheme name 'Bearer' we registered in Swagger
            try {
              ui.preauthorizeApiKey('Bearer', bearer);
              console && console.log && console.log('[swagger-custom] preauthorizeApiKey applied');
            } catch (e) {
              console && console.warn && console.warn('[swagger-custom] preauthorizeApiKey failed', e);
            }
          }
        } catch (ex) {
          console && console.warn && console.warn('[swagger-custom] tryPreauthorize ex', ex);
        }
      }

      function setupInterceptor() {
        try {
          if (window.ui && window.ui.getConfigs) {
            var cfg = window.ui.getConfigs();
            var previous = cfg.requestInterceptor;
            cfg.requestInterceptor = function (req) {
              if (!req.headers) req.headers = {};
              // Only set Authorization if not already set
              if (!req.headers['Authorization'] && !req.headers['authorization']) {
                req.headers['Authorization'] = bearer;
              }
              if (previous) return previous(req) || req;
              return req;
            };
            console && console.log && console.log('[swagger-custom] requestInterceptor installed');
            // Also attempt preauthorization directly on the ui instance
            tryPreauthorize(window.ui);
            return true;
          }
        } catch (e) {
          console && console.warn && console.warn('[swagger-custom] setupInterceptor error', e);
        }
        return false;
      }

      var tries = 0;
      (function waitForUI() {
        if (!setupInterceptor() && tries++ < 100) {
          setTimeout(waitForUI, 150);
        } else if (tries >= 100) {
          console && console.warn && console.warn('[swagger-custom] giving up waiting for Swagger UI');
        }
      })();
    })
    .catch(function (err) {
      console && console.warn && console.warn('[swagger-custom] Could not fetch automatic token:', err);
    });
})();