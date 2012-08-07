delete from Event

set identity_insert Event on
insert into Event (K, Name, DateTime, VenueK, IsTicketsAvailable, Lat, Lon) values (1, 'Test Ticketed Event', dateadd(day, 0, datediff(day, 0, getdate()) + 7), 1, 1, 1.1, 1.1)
INSERT INTO [Event](
	K
	,Name
	,ShortDetailsHtml
	,LongDetailsHtml
	,LongDetailsPlain
	,Pic
	,DateTime
	,VenueK
	,AdminNote
	,Capacity
	,OwnerUsrK
	,PicNew
	,TotalComments
	,TotalCommentsShallow
	,LastPostShallow
	,LastPost
	,AverageCommentDateTime
	,AverageCommentDateTimeShallow
	,TotalPhotos
	,LivePhotos
	,AddedDateTime
	,HasGuestlist
	,LastLivePhoto
	,HasSpotter
	,GuestlistOpen
	,GuestlistFinished
	,GuestlistLimit
	,GuestlistCount
	,GuestlistDetails
	,GuestlistPromoterK
	,GuestlistRegularPrice
	,GuestlistPrice
	,GuestlistPromotion
	,StartTime
	,AdminEmail
	,Donated
	,IsDescriptionText
	,IsNew
	,IsDescriptionCleanHtml
	,IsEdited
	,DuplicateGuid
	,PicState
	,PicPhotoK
	,PicMiscK
	,UrlFragment
	,MusicTypesString
	,ModeratorUsrK
	,MustBuyTicket
	,BuyableLockDateTime
	,IsTicketsAvailable
	,TicketHeat
	,HasHilight
	,UsrAttendCount
	,FixedDiscount
	,IsPriceFixed
	,Lat
	,Lon
	
)VALUES(
	2,
	'Spam night',--<Name, varchar(200),>
	'EAT SPAM!',--,<ShortDetailsHtml, text,>
	'We gonna eat da spam',--,<LongDetailsHtml, text,>
	'0',--,<LongDetailsPlain, bit,>
	null,--,<Pic, uniqueidentifier,>
	'2007-10-01 00:00:00.000',--,<DateTime, datetime,>
	'2',--,<VenueK, int,>
	'Event added by owner 10/10/2007 12:04:01',--,<AdminNote, text,>
	'25',--,<Capacity, int,>
	'1',--,<OwnerUsrK, int,>
	null,--,<PicNew, uniqueidentifier,>
	'0',--,<TotalComments, int,>
	null,--,<TotalCommentsShallow, int,>
	null,--,<LastPostShallow, datetime,>
	null,--,<LastPost, datetime,>
	null,--,<AverageCommentDateTime, datetime,>
	null,--,<AverageCommentDateTimeShallow, datetime,>
	null,--,<TotalPhotos, int,>
	null,--,<LivePhotos, int,>
	'2007-10-10 12:04:01.167',--,<AddedDateTime, datetime,>
	null,--,<HasGuestlist, bit,>
	null,--,<LastLivePhoto, datetime,>
	null,--,<HasSpotter, bit,>
	null,--,<GuestlistOpen, bit,>
	null,--,<GuestlistFinished, bit,>
	null,--,<GuestlistLimit, int,>
	null,--,<GuestlistCount, int,>
	null,--,<GuestlistDetails, text,>
	null,--,<GuestlistPromoterK, int,>
	null,--,<GuestlistRegularPrice, float,>
	null,--,<GuestlistPrice, float,>
	null,--,<GuestlistPromotion, bit,>
	4,--,<StartTime, int,>
	null,--,<AdminEmail, varchar(50),>
	null,--,<Donated, bit,>
	null,--,<IsDescriptionText, bit,>
	null,--,<IsNew, bit,>
	1,--,<IsDescriptionCleanHtml, bit,>
	null,--,<IsEdited, bit,>
	'F0A7AC80-CDC6-46D5-95FC-4A947111523D',--,<DuplicateGuid, uniqueidentifier,>
	null,--,<PicState, varchar(100),>
	null,--,<PicPhotoK, int,>
	null,--,<PicMiscK, int,>
	'uk/london/ministry-of-sound',--,<UrlFragment, varchar(255),>
	'All Music',--,<MusicTypesString, text,>
	null,--,<ModeratorUsrK, int,>
	null,--,<MustBuyTicket, bit,>
	null,--,<BuyableLockDateTime, datetime,>
	null,--,<IsTicketsAvailable, bit,>
	null,--,<TicketHeat, float,>
	null,--,<HasHilight, bit,>
	null,--,<UsrAttendCount, int,>
	null,--,<FixedDiscount, float,>
	null,--,<IsPriceFixed, bit,>,
	1,
	1
)
set identity_insert Event off


