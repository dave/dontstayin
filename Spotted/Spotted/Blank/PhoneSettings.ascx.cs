using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class PhoneSettings : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Phone p = null;

			if (ContainerPage.Url[0].Raw.Length>4)
				p = Phone.GetFromMac(ContainerPage.Url[0].Raw);
			else
				p = Phone.GetFromExtention(ContainerPage.Url[0].Raw);

			
			string settings = @"

# DSI phone settings...

language&: English(UK)
timezone&: GBR-0
date_us_format&: off
time_24_format&: on
setting_server&: http://www.dontstayin.com/popup/phonesettings/{mac}
ip_adr&: " + p.LocalIpAddress + @"
netmask&: 255.255.0.0
dns_domain&: dsihome.net
dns_server1&: " + p.LocalDns + @"
dhcp&: off
gateway&: " + p.LocalGateway + @"
phone_name&: phone-" + p.Usr.NickName.ToLower() + @"
ntp_server&: ntp.provu.co.uk
http_port!: " + (p.NatPort == null || p.NatPort.Length == 0 ? "80" : p.NatPort) + @"
http_user!: 
http_pass!: 
http_scheme!: off
https_port!: 443
webserver_type!: http_https

active_line!: 1
outgoing_identity!: 1
challenge_response!: off
refer_brackets!: off
register_http_contact!: off
cmc_feature!: off
filter_registrar!: on
xml_notify!: on
challenge_reboot!: off
challenge_checksync!: off

auto_dial!: off
dnd_mode!: off
privacy_in!: off
privacy_out!: off
admin_mode!: on
tone_scheme!: GBR

action_dnd_on_url!: 
action_dnd_off_url!: 
action_redirection_on_url!: 
action_redirection_off_url!: 
action_incoming_url!: 
action_outgoing_url!: 
action_setup_url!: 
action_offhook_url!: 
action_onhook_url!: http://www.dontstayin.com/support/phoneutility.aspx?type=hangup&mac=" + p.Mac + @"
action_missed_url!: 
action_connected_url!: 
action_disconnected_url!: http://www.dontstayin.com/support/phoneutility.aspx?type=hangup&mac=" + p.Mac + @"

auto_connect_type!: auto_connect_type_handsfree
auto_connect_indication!: on
logon_wizard&: off
guess_number!: off
guess_start_length!: 4

friends_ring_sound!: Ringer1
family_ring_sound!: Ringer1
colleagues_ring_sound!: Ringer1
vip_ring_sound!: Ringer3

break_key!: false
publish_presence!: on
edit_alpha_mode!: 123
display_method!: display_name
call_waiting!: visual
cw_dialtone!: on
disable_speaker!: off
no_dnd!: off
mute!: off
update_policy!: auto_update
conf_hangup!: on
mwi_notification!: silent
dnd_mode!: off

block_url_dialing!: off
release_sound!: off
deny_all_feature!: off
transfer_on_hangup!: on

ringer_headset_device!: speaker
dtmf_speaker_phone!: on
presence_timeout!: 15

firmware_status&: http://snom.provu.co.uk/firmware.php?version=6.2.3&linux=3.25&rootfs=jffs2
firmware_interval&: 15
firmware!: http://snom.provu.co.uk/sw/snom360-ramdiskToJffs2-3.36-br.bin

web_language!: English
call_completion!: off
callpickup_dialoginfo!: on
use_backlight!: on

call_join_xfer&: on
alert_info_playback!: on
ringing_time!: 60
silence_compression!: off
screen_saver_timeout!: 60
intercom_enabled!: on

keytones!: off

answer_after_policy!: idle
keyboard_lock!: off
ringer_animation!: on
speaker_dialer!: on

cancel_on_hold!: off
keyboard_lock_emergency!: 911 112 110 999 19222
cancel_missed!: on
cancel_desktop!: off

user_active1!: on
user_realname1!: " + p.Usr.FullName + @"
user_name1!: " + p.Extention.ToString() + @"
user_host1!: 192.168.16.127
user_pname1!: " + p.Extention.ToString() + @"
user_pass1!: 000" + p.Extention.ToString() + @"
user_mailbox1!: *123
user_idle_text1!: " + p.Usr.FullName + @"
user_ringer1!: Ringer10
user_outbound1!: 192.168.16.127


user_active2!: off
user_name2!: 1
user_host2!: 1
user_idle_text2!: Press the 'snom'

user_active3!: off
user_name3!: 1
user_host3!: 1
user_idle_text3!: button to pick

user_active4!: off
user_name4!: 1
user_host4!: 1
user_idle_text4!: up another line!




record_missed_calls1!: off

dkey_snom!: speed *8
fkey0!: dest <sip:201@192.168.16.127;user=phone>
fkey1!: dest <sip:204@192.168.16.127;user=phone>
fkey2!: dest <sip:210@192.168.16.127;user=phone>
fkey3!: dest <sip:208@192.168.16.127;user=phone> 
fkey4!: dest <sip:220@192.168.16.127;user=phone> 
fkey5!: url http://www.dontstayin.com/support/phoneutility.aspx?type=register&mac=$mac
fkey6!: dest <sip:200@192.168.16.127;user=phone>
fkey7!: dest <sip:202@192.168.16.127;user=phone>
fkey8!: dest <sip:207@192.168.16.127;user=phone>
fkey9!: dest <sip:211@192.168.16.127;user=phone>
fkey10!: dest <sip:206@192.168.16.127;user=phone> 
fkey11!: line 
fkey12!: line
fkey13!: line
fkey14!: line
fkey15!: line
fkey16!: line
fkey17!: line
fkey18!: line
fkey19!: line
fkey20!: line
fkey21!: line
fkey22!: line
fkey23!: line
fkey24!: line
fkey25!: line
fkey26!: line
fkey27!: line
fkey28!: line
fkey29!: line
fkey30!: line
fkey31!: line
fkey32!: line
fkey33!: line
fkey34!: line
fkey35!: line
fkey36!: line
fkey37!: line
fkey38!: line
fkey39!: line
fkey40!: line
fkey41!: line
fkey42!: line
fkey43!: line
fkey44!: line
fkey45!: line
fkey46!: line
fkey47!: line
fkey48!: line
fkey49!: line
fkey50!: line
fkey51!: line
fkey52!: line
fkey53!: line
fkey_context0!: active
fkey_context1!: active
fkey_context2!: active
fkey_context3!: active
fkey_context4!: active
fkey_context5!: active
fkey_context6!: active
fkey_context7!: active
fkey_context8!: active
fkey_context9!: active
fkey_context10!: active
fkey_context11!: active
fkey_context12!: active
fkey_context13!: active
fkey_context14!: active
fkey_context15!: active
fkey_context16!: active
fkey_context17!: active
fkey_context18!: active
fkey_context19!: active
fkey_context20!: active
fkey_context21!: active
fkey_context22!: active
fkey_context23!: active
fkey_context24!: active
fkey_context25!: active
fkey_context26!: active
fkey_context27!: active
fkey_context28!: active
fkey_context29!: active
fkey_context30!: active
fkey_context31!: active
fkey_context32!: active
fkey_context33!: active
fkey_context34!: active
fkey_context35!: active
fkey_context36!: active
fkey_context37!: active
fkey_context38!: active
fkey_context39!: active
fkey_context40!: active
fkey_context41!: active
fkey_context42!: active
fkey_context43!: active
fkey_context44!: active
fkey_context45!: active
fkey_context46!: active
fkey_context47!: active
fkey_context48!: active
fkey_context49!: active
fkey_context50!: active
fkey_context51!: active
fkey_context52!: active
fkey_context53!: active
tn_0!: David Brophy
tn_1!: John Brophy
tn_2!: Tim Aylott
tn_3!: George Thatcher
tn_4!: Jason Willans
tn_5!: Owain Harries
tn_6!: Andy Nelson
tn_7!: Brad Tong
tn_8!: Danny Stewart
tu_0!: 02070990200
tu_1!: 02070990201
tu_2!: 02070990202
tu_3!: 02070990204
tu_4!: 02070990207
tu_5!: 02070990210
tu_6!: 02070990211
tu_7!: 02070990208
tu_8!: 02070990206
tc_0!: vip
tc_1!: vip
tc_2!: vip
tc_3!: vip
tc_4!: vip
tc_5!: vip
tc_6!: vip
tc_7!: vip
tc_8!: vip
to_0!: active
to_1!: active
to_2!: active
to_3!: active
to_4!: active
to_5!: active
to_6!: active
to_7!: active
to_8!: active
";
			Response.Write(settings);
			Response.End();
		}
	}
}
