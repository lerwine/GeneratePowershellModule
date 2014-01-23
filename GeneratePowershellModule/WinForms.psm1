#region Import Required Assemblies

[Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms') | Out-Null;
[Reflection.Assembly]::LoadWithPartialName('System.Drawing') | Out-Null;

#endregion

Function New-Control {
	[OutputType([System.Windows.Forms.Control])]
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDefaultActionDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleName,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AccessibleRole]$AccessibleRole,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowDrop,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AnchorStyles]$Anchor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollOffset,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$BackColor,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$BackgroundImage,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageLayout]$BackgroundImageLayout,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BindingContext]$BindingContext,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$Bounds,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Capture,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CausesValidation,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CheckForIllegalCrossThreadCalls,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$ClientSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenu]$ContextMenu,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenuStrip]$ContextMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Cursor]$Cursor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DockStyle]$Dock,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Enabled,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Font]$Font,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$ForeColor,

		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum1')]
		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum3')]
		[System.Int32]$Height,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsAccessible,

		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum1')]
		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum3')]
		[System.Int32]$Left,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$Location,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Margin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MaximumSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MinimumSize,

		[Parameter(Mandatory=$False)]
		[System.String]$Name,

		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum1')]
		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum2')]
		[System.Windows.Forms.Control]$Parent,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Region]$Region,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.RightToLeft]$RightToLeft,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$Size,

		[Parameter(Mandatory=$False)]
		[System.Int32]$TabIndex,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TabStop,

		[Parameter(Mandatory=$False)]
		[System.Object]$Tag,

		[Parameter(Mandatory=$False)]
		[System.String]$Text,

		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum1')]
		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum3')]
		[System.Int32]$Top,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseWaitCursor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Visible,

		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum1')]
		[ParaParametermter(Mandatory=$False, ParameterSetName='setNum3')]
		[System.Int32]$Width,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IWindowTarget]$WindowTarget,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Padding,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImeMode]$ImeMode,

		[Parameter(Mandatory=$True)]
		[System.String]$text,

		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum2')]
		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum4')]
		[System.Int32]$left,

		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum2')]
		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum4')]
		[System.Int32]$top,

		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum2')]
		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum4')]
		[System.Int32]$width,

		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum2')]
		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum4')]
		[System.Int32]$height,

		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum3')]
		[ParaParametermter(Mandatory=$True, ParameterSetName='setNum4')]
		[System.Windows.Forms.Control]$parent

	}

}

Function New-ScrollableControl {
	[OutputType([System.Windows.Forms.ScrollableControl])]
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoScroll,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMargin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollPosition,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMinSize,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDefaultActionDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleName,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AccessibleRole]$AccessibleRole,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowDrop,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AnchorStyles]$Anchor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollOffset,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$BackColor,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$BackgroundImage,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageLayout]$BackgroundImageLayout,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BindingContext]$BindingContext,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$Bounds,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Capture,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CausesValidation,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$ClientSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenu]$ContextMenu,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenuStrip]$ContextMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Cursor]$Cursor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DockStyle]$Dock,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Enabled,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Font]$Font,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$ForeColor,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Height,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsAccessible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Left,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$Location,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Margin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MaximumSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MinimumSize,

		[Parameter(Mandatory=$False)]
		[System.String]$Name,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$Parent,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Region]$Region,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.RightToLeft]$RightToLeft,

		[Parameter(Mandatory=$False)]
		[System.ComponentModel.ISite]$Site,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$Size,

		[Parameter(Mandatory=$False)]
		[System.Int32]$TabIndex,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TabStop,

		[Parameter(Mandatory=$False)]
		[System.Object]$Tag,

		[Parameter(Mandatory=$False)]
		[System.String]$Text,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Top,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseWaitCursor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Visible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Width,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IWindowTarget]$WindowTarget,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Padding,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImeMode]$ImeMode

	}

}

Function New-Label {
	[OutputType([System.Windows.Forms.Label])]
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoSize,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoEllipsis,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$BackgroundImage,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageLayout]$BackgroundImageLayout,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BorderStyle]$BorderStyle,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.FlatStyle]$FlatStyle,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$Image,

		[Parameter(Mandatory=$False)]
		[System.Int32]$ImageIndex,

		[Parameter(Mandatory=$False)]
		[System.String]$ImageKey,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageList]$ImageList,

		[Parameter(Mandatory=$False)]
		[System.Drawing.ContentAlignment]$ImageAlign,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImeMode]$ImeMode,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TabStop,

		[Parameter(Mandatory=$False)]
		[System.Drawing.ContentAlignment]$TextAlign,

		[Parameter(Mandatory=$False)]
		[System.String]$Text,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseCompatibleTextRendering,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseMnemonic,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDefaultActionDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleName,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AccessibleRole]$AccessibleRole,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowDrop,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AnchorStyles]$Anchor,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollOffset,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$BackColor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BindingContext]$BindingContext,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$Bounds,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Capture,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CausesValidation,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$ClientSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenu]$ContextMenu,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenuStrip]$ContextMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Cursor]$Cursor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DockStyle]$Dock,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Enabled,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Font]$Font,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$ForeColor,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Height,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsAccessible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Left,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$Location,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Margin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MaximumSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MinimumSize,

		[Parameter(Mandatory=$False)]
		[System.String]$Name,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$Parent,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Region]$Region,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.RightToLeft]$RightToLeft,

		[Parameter(Mandatory=$False)]
		[System.ComponentModel.ISite]$Site,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$Size,

		[Parameter(Mandatory=$False)]
		[System.Int32]$TabIndex,

		[Parameter(Mandatory=$False)]
		[System.Object]$Tag,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Top,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseWaitCursor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Visible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Width,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IWindowTarget]$WindowTarget,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Padding

	}

}

Function New-ContainerControl {
	[OutputType([System.Windows.Forms.ContainerControl])]
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$False)]
		[System.Drawing.SizeF]$AutoScaleDimensions,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AutoScaleMode]$AutoScaleMode,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AutoValidate]$AutoValidate,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BindingContext]$BindingContext,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$ActiveControl,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoScroll,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMargin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollPosition,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMinSize,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDefaultActionDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleName,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AccessibleRole]$AccessibleRole,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowDrop,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AnchorStyles]$Anchor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollOffset,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$BackColor,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$BackgroundImage,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageLayout]$BackgroundImageLayout,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$Bounds,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Capture,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CausesValidation,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$ClientSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenu]$ContextMenu,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenuStrip]$ContextMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Cursor]$Cursor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DockStyle]$Dock,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Enabled,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Font]$Font,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$ForeColor,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Height,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsAccessible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Left,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$Location,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Margin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MaximumSize,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MinimumSize,

		[Parameter(Mandatory=$False)]
		[System.String]$Name,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$Parent,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Region]$Region,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.RightToLeft]$RightToLeft,

		[Parameter(Mandatory=$False)]
		[System.ComponentModel.ISite]$Site,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$Size,

		[Parameter(Mandatory=$False)]
		[System.Int32]$TabIndex,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TabStop,

		[Parameter(Mandatory=$False)]
		[System.Object]$Tag,

		[Parameter(Mandatory=$False)]
		[System.String]$Text,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Top,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseWaitCursor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Visible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Width,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IWindowTarget]$WindowTarget,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Padding,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImeMode]$ImeMode

	}

}

Function New-Form {
	[OutputType([System.Windows.Forms.Form])]
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IButtonControl]$AcceptButton,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowTransparency,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoScale,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScaleBaseSize,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoScroll,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AutoSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AutoSizeMode]$AutoSizeMode,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AutoValidate]$AutoValidate,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$BackColor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.FormBorderStyle]$FormBorderStyle,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IButtonControl]$CancelButton,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$ClientSize,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$ControlBox,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$DesktopBounds,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$DesktopLocation,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DialogResult]$DialogResult,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$HelpButton,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Icon]$Icon,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsMdiContainer,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$KeyPreview,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$Location,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MaximumSize,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.MenuStrip]$MainMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Margin,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.MainMenu]$Menu,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$MinimumSize,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$MaximizeBox,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Form]$MdiParent,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$MinimizeBox,

		[Parameter(Mandatory=$False)]
		[System.Double]$Opacity,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Form]$Owner,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$RightToLeftLayout,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$ShowInTaskbar,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$ShowIcon,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$Size,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.SizeGripStyle]$SizeGripStyle,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.FormStartPosition]$StartPosition,

		[Parameter(Mandatory=$False)]
		[System.Int32]$TabIndex,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TabStop,

		[Parameter(Mandatory=$False)]
		[System.String]$Text,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TopLevel,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$TopMost,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$TransparencyKey,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.FormWindowState]$WindowState,

		[Parameter(Mandatory=$False)]
		[System.Drawing.SizeF]$AutoScaleDimensions,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AutoScaleMode]$AutoScaleMode,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.BindingContext]$BindingContext,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$ActiveControl,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMargin,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollPosition,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Size]$AutoScrollMinSize,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDefaultActionDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleDescription,

		[Parameter(Mandatory=$False)]
		[System.String]$AccessibleName,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AccessibleRole]$AccessibleRole,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$AllowDrop,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.AnchorStyles]$Anchor,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Point]$AutoScrollOffset,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Image]$BackgroundImage,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImageLayout]$BackgroundImageLayout,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Rectangle]$Bounds,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Capture,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$CausesValidation,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenu]$ContextMenu,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ContextMenuStrip]$ContextMenuStrip,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Cursor]$Cursor,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.DockStyle]$Dock,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Enabled,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Font]$Font,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Color]$ForeColor,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Height,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$IsAccessible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Left,

		[Parameter(Mandatory=$False)]
		[System.String]$Name,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Control]$Parent,

		[Parameter(Mandatory=$False)]
		[System.Drawing.Region]$Region,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.RightToLeft]$RightToLeft,

		[Parameter(Mandatory=$False)]
		[System.ComponentModel.ISite]$Site,

		[Parameter(Mandatory=$False)]
		[System.Object]$Tag,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Top,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$UseWaitCursor,

		[Parameter(Mandatory=$False)]
		[System.Boolean]$Visible,

		[Parameter(Mandatory=$False)]
		[System.Int32]$Width,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.IWindowTarget]$WindowTarget,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.Padding]$Padding,

		[Parameter(Mandatory=$False)]
		[System.Windows.Forms.ImeMode]$ImeMode

	}

}
