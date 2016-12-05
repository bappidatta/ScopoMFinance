-- =========================================================================================
--		This script will declare variable for use in all ids in order to prevent having
--		to write custom unique GUIDs every time one is needed.
--
--	** NOTE: This file must be declared at the top of the pre-load data execution script **
-- =========================================================================================

DECLARE

	

	@Role_HOUser NVARCHAR (128) = 'A82C294F-7240-488B-9353-7DD4EADFE235',
	@Role_BranchUser  NVARCHAR (128) = 'F069FF9F-403F-48AE-809D-255FE6204897',

	--users---------------------
	@User_HOUser NVARCHAR (128) = '8991D45A-2832-4F68-AACC-544B0D864EB6',
	@User_BranchUser_101 NVARCHAR (128) = '8B61B9AD-A15B-4628-B778-7EB84C9D86B1',
	@User_BranchUser_102 NVARCHAR (128) = '0BECCD9C-4934-48F3-8369-59F30D8907A6',

	----------------------------

	--branches-------------
	@Branch_HO INT = 1,
	@Branch_101 INT = 2,
	@Branch_102 INT = 3,

	----------------------------

	--Donor----------------
	@Donor_1 INT = 1,
	@Donor_2 INT = 2,

	----------------------------
	
	@LoremIpsum1 nvarchar(MAX) = '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ornare dignissim ipsum nec fringilla. Curabitur id dui posuere, fringilla eros nec, hendrerit tellus. Nulla facilisi. Donec venenatis enim et nibh elementum, quis porttitor ante pellentesque. Ut lacinia convallis felis, vel pharetra mi. Sed sed purus malesuada, vulputate purus et, ultrices lacus. Vivamus eu placerat nibh. Donec felis odio, tincidunt vel urna eget, consectetur iaculis dui. Nullam non tortor sed arcu semper scelerisque. Integer nisl sem, luctus a lacus blandit, consectetur luctus lorem.</p><p>Nunc gravida, magna et ornare maximus, erat risus suscipit felis, et ultrices nisi lacus sed ipsum. Sed venenatis tristique tempus. Pellentesque quam orci, gravida vel tellus at, pellentesque facilisis magna. Vivamus ut ipsum sed nisi porttitor condimentum sit amet id felis. Suspendisse consectetur sapien vel ullamcorper pharetra. Nulla id mauris sapien. Donec quis enim erat. Nulla dapibus hendrerit justo nec aliquet. Nullam consectetur sapien nec dolor viverra hendrerit.</p><p>In vel tempus ipsum. Pellentesque quis tempor augue, nec feugiat ex. Vivamus iaculis, risus quis lacinia gravida, tellus mi fringilla quam, et condimentum odio magna eget tortor. Praesent porttitor vulputate nisi a tincidunt. In mollis leo lacus, sed vulputate lorem pellentesque consectetur. Etiam vitae lorem a nisi blandit finibus. Sed suscipit est id ultrices consequat. Vivamus sed euismod mi. Morbi porttitor laoreet semper. Integer non convallis sapien. Morbi volutpat molestie volutpat. Vivamus efficitur est vitae odio pulvinar dictum. Nullam interdum mauris a elit commodo, nec placerat nibh pharetra. Fusce eu aliquet justo. Pellentesque aliquam velit eget ex hendrerit, eu porttitor nunc efficitur. Etiam dui metus, ornare suscipit venenatis in, suscipit in nulla.</p><p>Sed quis placerat mi. Aenean aliquam gravida tellus, in pharetra erat convallis eget. Nullam interdum odio ac fringilla placerat. Etiam pellentesque mattis sem non dignissim. Donec vel pretium nibh. Etiam eget lectus in massa pretium accumsan. Phasellus vehicula justo id libero facilisis lobortis. Aenean quis odio feugiat, accumsan urna id, rutrum magna. Cras vulputate eget mi tincidunt luctus.</p><p>Interdum et malesuada fames ac ante ipsum primis in faucibus. Vivamus mollis eleifend mauris, at dapibus nunc dictum in. Aenean pulvinar faucibus sodales. Sed sed pellentesque arcu. Donec id blandit ipsum. Ut iaculis velit mauris, volutpat condimentum libero pharetra at. Etiam finibus blandit turpis, in elementum nisl semper sed. In hac habitasse platea dictumst. In sit amet vulputate nisl. Curabitur lobortis interdum arcu, at condimentum neque pharetra eu.</p>',
	@LoremIpsum2 nvarchar(MAX) = '<p>In qui insolens volutpat, pri cu veri homero melius. At epicuri quaerendum vix, paulo mandamus incorrupte pri te! Has atomorum inciderint at, id eum quis eleifend sententiae. Qui timeam perfecto percipitur eu, vim vero admodum cu. Ius in meis expetendis, vim at nusquam vivendo deterruisset. Officiis inciderint philosophia cu usu, vitae placerat mea te!</p><p>Ferri invidunt sit id. Modo probatus id duo, quod purto paulo eam te. Eu duo homero doctus facilis. Melius fabellas postulant cu eam, eu sea expetenda intellegam. Stet aeterno conclusionemque vis at, sonet ornatus copiosae ea est, cum causae temporibus te. His dicta recteque percipitur at, scripta euismod ornatus eu mel. Qui laoreet delicatissimi ei.</p><p>Est eu tritani adipiscing efficiendi, sed no sint phaedrum, quo everti consulatu dissentiet at? Quaeque accusam dissentias in vim. Cu sea vivendo ancillae mnesarchum, id perfecto maluisset sea. Utroque reformidans id ius. Sumo offendit principes ius no.</p>',

	@LoremIpsum1Short nvarchar(MAX) = 'Maecenas luctus lobortis elit, vitae convallis elit suscipit sed. Etiam efficitur aliquam elit rhoncus consectetur. Nulla est magna, vehicula id hendrerit eget, mattis id orci. Sed id sagittis nullam.',
	@LoremIpsum2Short nvarchar(MAX) = 'Fusce id augue vel ante egestas rutrum. Nullam ullamcorper purus eget placerat dictum. Suspendisse nec lectus leo. Integer feugiat purus non mauris scelerisque pulvinar. Sed pulvinar purus velit amet.',

	@DateTime1 DateTime = '20150110 10:34:09 AM',
	@DateTime2 DateTime = '20150113 4:22:55 PM'