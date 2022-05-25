class SharedWatchedStatusPluginConfiguration
{
    UserConnections;
}

class SharedWatchedStatusConfigurationPage
{
    pluginUniqueId = 'af7e610f-ed05-4efd-bd69-c2269cf701f2';

    constructor ()
    {
        const pageElement = document.getElementById('SharedWatchedStatusConfigurationPage');
        pageElement.addEventListener('pageshow', this.onPageshow.bind(this)); // FIXME: Dangling promise

        //const formElement = document.getElementById('SharedWatchedStatusConfigurationForm');
    }

    async onPageshow ()
    {
        Dashboard.showLoadingMsg();

        /** @type { SharedWatchedStatusPluginConfiguration } */
        const pluginConfiguration = await ApiClient.getPluginConfiguration(this.pluginUniqueId);

        console.log(pluginConfiguration);

        // document.querySelector('#Options').value = config.Options;

        Dashboard.hideLoadingMsg();
    }
};

const sharedWatchedStatusConfigurationPage = new SharedWatchedStatusConfigurationPage();

/*
ApiClient.getUsers().then(
    function (users)
    {
        let html = '';
        for (let i = 0, length = users.length; i < length; i++)
        {
            const user = users[i];
            html += '<option value="' + user.Id + '">' + user.Name + '</option>';
        }

        document.getElementById('selectUserFrom').innerHTML = html;
    }
);
*/

/*
document.querySelector('#SharedWatchedStatusConfigForm')
    .addEventListener(
        'submit',
        function ()
        {
            Dashboard.showLoadingMsg();
            ApiClient.getPluginConfiguration(SharedWatchedStatusConfig.pluginUniqueId)
                .then(
                    function (config)
                    {
                        config.Options = document.querySelector('#Options').value;
                        config.AnInteger = document.querySelector('#AnInteger').value;
                        config.TrueFalseSetting = document.querySelector('#TrueFalseSetting').checked;
                        config.AString = document.querySelector('#AString').value;
                        ApiClient.updatePluginConfiguration(SharedWatchedStatusConfig.pluginUniqueId, config)
                            .then(
                                function (result)
                                {
                                    Dashboard.processPluginConfigurationUpdateResult(result);
                                }
                        );
                    }
            );

            return false;
        }
);
*/
