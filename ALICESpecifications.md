# Development Tasks
Explore the agent functionality in the OpenAI API. Modify the code as needed. 

The files Connection.vb and ALICESpecifications.md  are intended  for the development of the component for the ALICE project. They can be ignored by other users. 

## Database structure
1. Save the JSON and Markup in the table ARTIFICIALIS. Check the console in the browser's development tools to verify these data. 
2. When the page loads, retrieve all entries from the table ARTIFICIALIS by the primary key {id_opus, id_pagina, id_nauta, am_ordo}. Fix id_opus, id_pagina, id_nauta.  Pass the informaiton to OpenAI to create the context for the chat. 
3. Add the functionality to delete a given answer from the OpenAI engine. Every response and query need a "delete" link. Manage it via AJAX.

